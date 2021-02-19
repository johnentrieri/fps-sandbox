using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthAmmount = 5;

    private PlayerHealth playerHealth;

    void Start() {        
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }
        if ( playerHealth.AddHealth(healthAmmount) ) {
            Destroy(gameObject);
        }
    }
}
