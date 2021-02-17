using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable] private class AmmoSlot {

        public AmmoType ammoType;
        public int ammoAmount;
        public int maxAmount = 99;
    }

    public int GetAmmoAmount(AmmoType ammoType) {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        if (ammoSlot == null) { return 0;}
        return ammoSlot.ammoAmount;
    }

    public bool DecreaseAmmo(AmmoType ammoType, int ammoDec) {

        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        if (ammoSlot == null) { return false; }

        if (ammoSlot.ammoAmount <= 0) { return false; }

        ammoSlot.ammoAmount -= ammoDec;
        if (ammoSlot.ammoAmount < 0) { ammoSlot.ammoAmount = 0; }

        return true;
    }

    public bool IncreaseAmmo(AmmoType ammoType, int ammoAdd) {

        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        if (ammoSlot == null) { return false; }

        if (ammoSlot.ammoAmount >= ammoSlot.maxAmount) { return false;}

        ammoSlot.ammoAmount += ammoAdd;

        if (ammoSlot.ammoAmount > ammoSlot.maxAmount) { 
            ammoSlot.ammoAmount = ammoSlot.maxAmount; 
        }

        return true;
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
