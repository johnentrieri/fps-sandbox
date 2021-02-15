using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable] private class AmmoSlot {

        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetAmmoAmount(AmmoType ammoType) {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        if (ammoSlot == null) { return 0;}
        return ammoSlot.ammoAmount;
    }

    public void SubtractAmmo(AmmoType ammoType, int ammoDec) {

        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        if (ammoSlot == null) { return; }

        ammoSlot.ammoAmount -= ammoDec;
        if (ammoSlot.ammoAmount < 0) { ammoSlot.ammoAmount = 0; }
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType) {
        foreach (AmmoSlot ammoSlot in ammoSlots) {
            if (ammoSlot.ammoType == ammoType) {
                return ammoSlot;
            }
        }
        return null;
    }
}
