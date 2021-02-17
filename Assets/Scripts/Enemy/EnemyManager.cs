using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[SerializeField] Canvas winnerCanvas;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float timeBetweenSpawns = 1.0f;
    [SerializeField] float timeBetweenWaves = 5.0f;

    private int waveNum, spawnedEnemies, enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        //winnerCanvas.enabled = false;
        //numEnemies = GetComponentsInChildren<Enemy>().Length;

        waveNum = 1;
        spawnedEnemies = waveNum * waveNum;
        enemiesRemaining = spawnedEnemies;
        StartCoroutine( StartNextWave() );
    }

    public void EnemyDeathHandler() {
        if( --enemiesRemaining <= 0) {
            Debug.Log("Completed Wave " + waveNum); 
            waveNum++;
            spawnedEnemies = waveNum * waveNum;
            enemiesRemaining = spawnedEnemies;
            StartCoroutine( StartNextWave() );
        }
    }

    /*
    private void ProcessWin() {
        winnerCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSelect>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    */

    private IEnumerator StartNextWave() {
        Debug.Log("Starting Wave " + waveNum);      
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine( SpawnEnemies() ); 
    }

    private IEnumerator SpawnEnemies() {
        for (int i=0; i < spawnedEnemies; i++) {
            Debug.Log("Spawning Enemy " + (i+1).ToString() + " of " + spawnedEnemies);
            Vector3 spawnPosition = ChooseRandomSpawnPoint().position;
            Instantiate(enemyPrefab,spawnPosition,Quaternion.identity,transform);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private Transform ChooseRandomSpawnPoint() {
        int rngSpawn = Random.Range(0,spawnPoints.Length);
        Debug.Log("Spawning From Spawn Point " + rngSpawn);
        return spawnPoints[rngSpawn];
    }
}
