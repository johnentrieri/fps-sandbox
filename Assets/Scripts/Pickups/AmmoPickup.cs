using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType = AmmoType.Bullets;
    [SerializeField] int ammoAmount = 100;
    private PickupManager pickupManager;
    private PickupSpawnPoint pickupSpawnPoint;

    private Ammo ammo;

    void Start() {
        pickupManager = GetComponentInParent<PickupManager>();
        pickupSpawnPoint = GetComponentInParent<PickupSpawnPoint>();
        ammo = FindObjectOfType<Ammo>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }
        if ( ammo.IncreaseAmmo(ammoType,ammoAmount) ) {
            pickupManager.UnpopulateSpawnPoint(pickupSpawnPoint);
            Destroy(gameObject);
        }
    }
}
