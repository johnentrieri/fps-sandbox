using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    private Weapon[] weapons;
    private int activeWeaponIndex = 0;

    void Start()
    {
        weapons = GetComponentsInChildren<Weapon>();

        for (int i = 1; i < weapons.Length; i++) {
            if (i ==0) { weapons[i].gameObject.SetActive(true); }
            else { weapons[i].gameObject.SetActive(false); }
        }

    }

    void Update() {
        if (Input.GetButtonDown("Fire3")) {
            CycleToNextWeapon();
        }
    }

    private void CycleToNextWeapon()
    {
        weapons[activeWeaponIndex].gameObject.SetActive(false);

        if ( weapons.Length == activeWeaponIndex + 1) {
            activeWeaponIndex = 0;
        } else {
            activeWeaponIndex++;
        }

        weapons[activeWeaponIndex].gameObject.SetActive(true);
    }

    private void ActivateWeapon(int index)
    {
        weapons[activeWeaponIndex].gameObject.SetActive(false);
        activeWeaponIndex = index;
        weapons[activeWeaponIndex].gameObject.SetActive(true);        
    }
}
