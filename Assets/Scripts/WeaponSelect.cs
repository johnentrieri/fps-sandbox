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
        ProcessScrollWheel();
        if (Input.GetButtonDown("WeaponCycle")) { CycleToNextWeapon(); }
        if (Input.GetButtonDown("Weapon1")) { ActivateWeapon(0); }
        if (Input.GetButtonDown("Weapon2")) { ActivateWeapon(1); }
        if (Input.GetButtonDown("Weapon3")) { ActivateWeapon(2); }
    }

    private void ProcessScrollWheel() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) { CycleToNextWeapon(); }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) { CycleToPreviousWeapon(); }
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

    private void CycleToPreviousWeapon()
    {
        weapons[activeWeaponIndex].gameObject.SetActive(false);

        if ( activeWeaponIndex == 0) {
            activeWeaponIndex = weapons.Length - 1;
        } else {
            activeWeaponIndex--;
        }

        weapons[activeWeaponIndex].gameObject.SetActive(true);
    }

    private void ActivateWeapon(int index)
    {
        if (index >= weapons.Length) { return; }
        if (index == activeWeaponIndex) { return; }

        weapons[activeWeaponIndex].gameObject.SetActive(false);
        activeWeaponIndex = index;
        weapons[activeWeaponIndex].gameObject.SetActive(true);
    }
}
