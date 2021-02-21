using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] EnemyType[] enemyTypes; 
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float timeBetweenWaves = 2.0f;
    private int waveNum, spawnedEnemies, enemiesRemaining;
    private EnemySpawnPoint[] spawnPoints;
    private PlayerUIHandler playerUIHandler;
    private PickupManager pickupManager;
    private EnemyType currentWaveType;

    [System.Serializable] class EnemyType {
        public GameObject enemyPrefab;
        public int earliestWave; //TODO
        public Queue<GameObject> enemyPool = new Queue<GameObject>();
    }

    void Start()
    {
        spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
        playerUIHandler = FindObjectOfType<PlayerUIHandler>();
        pickupManager = FindObjectOfType<PickupManager>();
        waveNum = 1;
        StartCoroutine( StartNextWave() );
    }

    public void EnemyDeathHandler(GameObject enemy) {
        currentWaveType.enemyPool.Enqueue(enemy);
        if( --enemiesRemaining <= 0) {
            waveNum++;
            StartCoroutine( StartNextWave() );
        }
    }

    private IEnumerator StartNextWave() {
        currentWaveType = ChooseEnemyType();
         
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
            if (currentWaveType.enemyPool.Count > 0) {
                RecycleEnemy(spawnPosition);
            } else {            
                Instantiate(currentWaveType.enemyPrefab,spawnPosition,Quaternion.identity,transform);
            }
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private EnemyType ChooseEnemyType() {
        int rngEnemyType = -1;
        do { rngEnemyType = Random.Range(0,enemyTypes.Length); }
        while ( enemyTypes[rngEnemyType].earliestWave > waveNum);
        
        return( enemyTypes[rngEnemyType] );
    }

    private Transform ChooseRandomSpawnPoint() {
        int rngSpawn = Random.Range(0,spawnPoints.Length);
        return spawnPoints[rngSpawn].transform;
    }

    private void RecycleEnemy(Vector3 newSpawnPosition) {
        GameObject recycledEnemy = currentWaveType.enemyPool.Dequeue();
        recycledEnemy.SetActive(false);
        recycledEnemy.transform.position = newSpawnPosition;
        recycledEnemy.GetComponent<Enemy>().Reanimate();
        recycledEnemy.SetActive(true);
    }
}
