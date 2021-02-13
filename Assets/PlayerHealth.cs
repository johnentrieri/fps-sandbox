using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int HP = 10;

    public void InflictDamage(int dmg) {
        HP -= dmg;

        if (HP <= 0) {
            ProcessDeath();
        }
    }

    private void ProcessDeath() {
        HP = 0;
        Debug.Log("You Have Died!");
    }
}
