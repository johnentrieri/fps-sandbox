using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    [SerializeField] int ammoAmount = 6;

    public int GetAmmoAmount() {
        return ammoAmount;
    }

    public int SubtractAmmo(int ammoDec) {
        ammoAmount -= ammoDec;
        if (ammoAmount <= 0) { ammoAmount = 0; }
        return ammoAmount;
    }
}
