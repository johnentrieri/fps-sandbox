using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float timeBetweenSpawns = 1.0f;
    [SerializeField] float timeBetweenWaves = 5.0f;

    private int waveNum, spawnedEnemies, enemiesRemaining;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    void Start()
    {
        waveNum = 1;
        spawnedEnemies = waveNum * waveNum;
        enemiesRemaining = spawnedEnemies;
        StartCoroutine( StartNextWave() );
    }

    public void EnemyDeathHandler(GameObject enemy) {
        enemyPool.Enqueue(enemy);
        if( --enemiesRemaining <= 0) {
            waveNum++;
            spawnedEnemies = waveNum * waveNum;
            enemiesRemaining = spawnedEnemies;
            StartCoroutine( StartNextWave() );
        }
    }

    private IEnumerator StartNextWave() {    
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
        return spawnPoints[rngSpawn];
    }

    private void RecycleEnemy(Vector3 newSpawnPosition) {
        GameObject recycledEnemy = enemyPool.Dequeue();
        recycledEnemy.SetActive(false);
        recycledEnemy.transform.position = newSpawnPosition;
        recycledEnemy.GetComponent<Enemy>().Reanimate();
        recycledEnemy.SetActive(true);
    }
}
