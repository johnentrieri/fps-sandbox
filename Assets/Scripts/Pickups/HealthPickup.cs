using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthAmmount = 5;

    private PlayerHealth playerHealth;
    private PickupManager pickupManager;
    private PickupSpawnPoint pickupSpawnPoint;

    void Start() {        
        playerHealth = FindObjectOfType<PlayerHealth>();
        pickupManager = GetComponentInParent<PickupManager>();
        pickupSpawnPoint = GetComponentInParent<PickupSpawnPoint>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }
        if ( playerHealth.AddHealth(healthAmmount) ) {
            pickupManager.UnpopulateSpawnPoint(pickupSpawnPoint);
            Destroy(gameObject);
        }
    }
}
