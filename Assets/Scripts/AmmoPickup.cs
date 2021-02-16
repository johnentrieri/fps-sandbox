using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount;

    private Ammo ammo;

    void Start() {
        ammo = FindObjectOfType<Ammo>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }
        if ( AddAmmo() ) {
            Destroy(gameObject);
        }
    }

    private bool AddAmmo() {
        return ammo.IncreaseAmmo(ammoType,ammoAmount);
    }
}
