using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int HP = 10;
    [SerializeField] ParticleSystem enemyHitEffect;
    [SerializeField] ParticleSystem deathEffect;

    public ParticleSystem GetHitEffect() {
        return enemyHitEffect;
    }

    public void InflictDamage(int dmg) {
        HP -= dmg;
        GetComponent<EnemyAI>().Provoke();

        if (HP <= 0) {
            ProcessDeath();
        }
    }

    private void ProcessDeath() {
        HP = 0;
        Destroy(gameObject);
    }
}
