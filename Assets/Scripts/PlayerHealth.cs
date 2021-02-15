using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int HP = 10;
    [SerializeField] Canvas gameOverCanvas;

    void Start() {
        gameOverCanvas.enabled = false;
    }

    public void InflictDamage(int dmg) {
        HP -= dmg;

        if (HP <= 0) {
            ProcessDeath();
        }
    }

    private void ProcessDeath() {
        HP = 0;
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSelect>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
