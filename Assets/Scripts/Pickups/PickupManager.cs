using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] Pickup[] pickups;
    private List<int> lottoPool = new List<int>();
    private List<PickupSpawnPoint> freeSpawnPoints = new List<PickupSpawnPoint>();

    [System.Serializable] private class Pickup {
        public GameObject pickupPrefab;
        public int spawnLottoEntries;
    }

    void Start() {
        PickupSpawnPoint[] spawnPoints = GetComponentsInChildren<PickupSpawnPoint>();
        for (int sIndex=0; sIndex < spawnPoints.Length; sIndex++) {
            if ( !spawnPoints[sIndex].isPopulated ) { 
                freeSpawnPoints.Add(spawnPoints[sIndex]);
            }
        }

        // Populate Lottery Pool      
        for (int pIndex=0; pIndex < pickups.Length; pIndex++ ) {
            for (int eIndex=0; eIndex < pickups[pIndex].spawnLottoEntries; eIndex++) {
                lottoPool.Add(pIndex);
            }
        }

    }

    public void ProcessPickupsForWave(int waveNum) {
        SpawnPickups(waveNum*2);
    }

    private void SpawnPickups(int quantity) {
        for (int i=0; i < quantity; i++) {
            if (freeSpawnPoints.Count <= 0) { return; }
            PopulateSpawnPoint( PullPickup(), GetRandomSpawnPoint());
        }
    }

    private Pickup PullPickup() {
        int lottoIndex = Random.Range(0,lottoPool.Count);
        int rngPickupIndex = lottoPool[lottoIndex];
        Pickup pickup = pickups[rngPickupIndex];
        return(pickup);
    }

    private PickupSpawnPoint GetRandomSpawnPoint() {
        int rngIndex = Random.Range(0,freeSpawnPoints.Count);
        return( freeSpawnPoints[rngIndex] );
    }
    
    public void UnpopulateSpawnPoint(PickupSpawnPoint spawnPoint) {
        spawnPoint.isPopulated = false;
        freeSpawnPoints.Add(spawnPoint);
    }

    private void PopulateSpawnPoint(Pickup pickup, PickupSpawnPoint spawnPoint) {
        Instantiate(pickup.pickupPrefab ,spawnPoint.transform);
        freeSpawnPoints.Remove(spawnPoint);
        spawnPoint.isPopulated = true;
    }

}
