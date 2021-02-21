using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] int ammoAmount;
    [SerializeField] float rotationSpeed = 180.0f;
    private WeaponSelect weaponSelect;
    private AmmoType ammoType;
    private PickupManager pickupManager;
    private PickupSpawnPoint pickupSpawnPoint;
    private Ammo ammo;

    void Start() {
        weaponSelect = FindObjectOfType<WeaponSelect>();
        pickupManager = GetComponentInParent<PickupManager>();
        pickupSpawnPoint = GetComponentInParent<PickupSpawnPoint>();
        ammoType = weaponPrefab.GetComponentInChildren<Weapon>().GetAmmoType();
        ammo = FindObjectOfType<Ammo>();
    }

    void Update() {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") { return; }
        Weapon weapon = weaponPrefab.GetComponentInChildren<Weapon>();
        weaponSelect.AddWeapon(weapon);
        ammo.IncreaseAmmo(ammoType,ammoAmount);
        pickupManager.UnpopulateSpawnPoint(pickupSpawnPoint);
        Destroy(gameObject);
    }
}
