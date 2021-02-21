using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int HP = 10;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Transform weaponDirectory;
    [SerializeField] float baseSpeed = 8;
    [SerializeField] float runningSpeed = 12;

    private int maxHP;
    private PlayerUIHandler playerUIHandler;
    private RigidbodyFirstPersonController fpsController;
    private bool isSprinting = false;

    void Start() {
        maxHP = HP;
        gameOverCanvas.enabled = false;
        playerUIHandler = FindObjectOfType<PlayerUIHandler>();
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
    }

    void Update() {
        if (Input.GetButtonDown("Sprint")) { ToggleSprint(); }
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


    private void ToggleSprint() {
        if (isSprinting) {
            fpsController.movementSettings.ForwardSpeed =baseSpeed;
            isSprinting = false;
        } else {
            fpsController.movementSettings.ForwardSpeed = runningSpeed;
            isSprinting = true;
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
