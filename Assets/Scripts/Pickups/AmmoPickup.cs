using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType = AmmoType.Bullets;
    [SerializeField] int ammoAmount = 100;

    private Ammo ammo;

    void Start() {
        ammo = FindObjectOfType<Ammo>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }
        if ( ammo.IncreaseAmmo(ammoType,ammoAmount) ) {
            Destroy(gameObject);
        }
    }
}
