using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] float rotationSpeed = 180.0f;
    private WeaponSelect weaponSelect;

    void Start() {
        weaponSelect = FindObjectOfType<WeaponSelect>();
    }

    void Update() {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }
        Weapon weapon = weaponPrefab.GetComponentInChildren<Weapon>();
        if ( weaponSelect.AddWeapon(weapon) ) {
            Destroy(gameObject);
        }
    }

}
