using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int HP = 10;
    [SerializeField] Canvas gameOverCanvas;

    private int maxHP;
    private PlayerUIHandler playerUIHandler;

    void Start() {
        maxHP = HP;
        gameOverCanvas.enabled = false;
        playerUIHandler = FindObjectOfType<PlayerUIHandler>();
    }

    public bool AddHealth(int healthAmount) {
        if (HP == maxHP) { return false; }
        if (HP < maxHP) { HP += healthAmount; }
        if (HP > maxHP) { HP = maxHP; }
        playerUIHandler.setUIHealthBar(HP, maxHP);
        return true;
    }

    public void InflictDamage(int dmg) {
        HP -= dmg;
        if (HP < 0) { HP = 0; }
        playerUIHandler.setUIHealthBar(HP, maxHP);

        if (HP <= 0) {
            ProcessDeath();
        }
    }

    private void ProcessDeath() {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSelect>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
