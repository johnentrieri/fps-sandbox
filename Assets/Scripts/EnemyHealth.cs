using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int HP = 10;

    // Start is called before the first frame update

    public void InflictDamage(int dmg) {
        HP -= dmg;

        if (HP <= 0) {
            ProcessDeath();
        }
    }

    private void ProcessDeath() {
        HP = 0;
        Destroy(gameObject);
    }
}
