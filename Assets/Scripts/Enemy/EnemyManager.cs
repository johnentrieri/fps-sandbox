using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] EnemySpawnPoint[] spawnPoints;
    [SerializeField] float timeBetweenSpawns = 1.0f;
    [SerializeField] float timeBetweenWaves = 5.0f;

    private int waveNum, spawnedEnemies, enemiesRemaining;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    private PlayerUIHandler playerUIHandler;
    private PickupManager pickupManager;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
        playerUIHandler = FindObjectOfType<PlayerUIHandler>();
        pickupManager = FindObjectOfType<PickupManager>();
        waveNum = 1;
        if (playerUIHandler != null) { playerUIHandler.SetWave(waveNum); }
        spawnedEnemies = waveNum;
        enemiesRemaining = spawnedEnemies;
        StartCoroutine( StartNextWave() );
    }

    public void EnemyDeathHandler(GameObject enemy) {
        enemyPool.Enqueue(enemy);
        if( --enemiesRemaining <= 0) {
            waveNum++;
            StartCoroutine( StartNextWave() );
        }
    }

    private IEnumerator StartNextWave() {
         
        spawnedEnemies = waveNum;
        enemiesRemaining = spawnedEnemies;   
        if (playerUIHandler != null) { playerUIHandler.SetWave(waveNum); }
        pickupManager.ProcessPickupsForWave(waveNum);
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine( SpawnEnemies() ); 
    }

    private IEnumerator SpawnEnemies() {
        for (int i=0; i < spawnedEnemies; i++) {
            Vector3 spawnPosition = ChooseRandomSpawnPoint().position;
            if (enemyPool.Count > 0) {
                RecycleEnemy(spawnPosition);
            } else {            
                Instantiate(enemyPrefab,spawnPosition,Quaternion.identity,transform);
            }
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private Transform ChooseRandomSpawnPoint() {
        int rngSpawn = Random.Range(0,spawnPoints.Length);
        return spawnPoints[rngSpawn].transform;
    }

    private void RecycleEnemy(Vector3 newSpawnPosition) {
        GameObject recycledEnemy = enemyPool.Dequeue();
        recycledEnemy.SetActive(false);
        recycledEnemy.transform.position = newSpawnPosition;
        recycledEnemy.GetComponent<Enemy>().Reanimate();
        recycledEnemy.SetActive(true);
    }
}
