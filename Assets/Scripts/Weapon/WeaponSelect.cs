using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] Transform weaponDirectory;
    private List<Weapon> weapons = new List<Weapon>();
    private int activeWeaponIndex = 0;

    void Start()
    {
        foreach( Weapon w in GetComponentsInChildren<Weapon>() ){
            weapons.Add(w);
        }

        for (int i = 0; i < weapons.Count; i++) {
            if (i == 0) { 
                weapons[i].gameObject.SetActive(true);
            } else { 
                WeaponZoom weaponZoom = weapons[activeWeaponIndex].GetComponent<WeaponZoom>();
                if (weaponZoom != null) { weaponZoom.ZoomOut(); }

                weapons[i].gameObject.SetActive(false);
            }
        }

    }

    void Update() {
        ProcessScrollWheel();
        if (Input.GetButtonDown("WeaponCycle")) { CycleToNextWeapon(); }
        if (Input.GetButtonDown("Weapon1")) { ActivateWeapon(0); }
        if (Input.GetButtonDown("Weapon2")) { ActivateWeapon(1); }
        if (Input.GetButtonDown("Weapon3")) { ActivateWeapon(2); }
    }

    public bool AddWeapon(Weapon weapon) {
        foreach(Weapon w in weapons) {
            if (weapon.weaponName == w.weaponName) { return false;}
        }
        Weapon spawnedWeapon = Instantiate(weapon,weaponDirectory);
        weapons.Add(spawnedWeapon);
        ActivateWeapon(weapons.Count-1);
        return true;
    }

    private void ProcessScrollWheel() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) { CycleToNextWeapon(); }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) { CycleToPreviousWeapon(); }
    }

    private void CycleToNextWeapon()
    {
        WeaponZoom weaponZoom = weapons[activeWeaponIndex].GetComponent<WeaponZoom>();
        if (weaponZoom != null) { weaponZoom.ZoomOut(); }

        weapons[activeWeaponIndex].gameObject.SetActive(false);

        if ( weapons.Count == activeWeaponIndex + 1) {
            activeWeaponIndex = 0;
        } else {
            activeWeaponIndex++;
        }

        weapons[activeWeaponIndex].gameObject.SetActive(true);
    }

    private void CycleToPreviousWeapon()
    {
        WeaponZoom weaponZoom = weapons[activeWeaponIndex].GetComponent<WeaponZoom>();
        if (weaponZoom != null) { weaponZoom.ZoomOut(); }

        weapons[activeWeaponIndex].gameObject.SetActive(false);

        if ( activeWeaponIndex == 0) {
            activeWeaponIndex = weapons.Count - 1;
        } else {
            activeWeaponIndex--;
        }

        weapons[activeWeaponIndex].gameObject.SetActive(true);
    }

    private void ActivateWeapon(int index)
    {
        if (index >= weapons.Count) { return; }
        if (index == activeWeaponIndex) { return; }

        WeaponZoom weaponZoom = weapons[activeWeaponIndex].GetComponent<WeaponZoom>();
        if (weaponZoom != null) { weaponZoom.ZoomOut(); }
        weapons[activeWeaponIndex].gameObject.SetActive(false);
        
        activeWeaponIndex = index;
        weapons[activeWeaponIndex].gameObject.SetActive(true);
    }
}
