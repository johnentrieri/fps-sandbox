using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Text weaponName;
    [SerializeField] Text ammoRemaining;
    [SerializeField] Text wave;
    [SerializeField] Text scoreboard;
    private int score = 0;

    void Start() {
        AddToScore(0);
    }

    public void setUIHealthBar(int HP, int maxHP) {
        float helthPercent = (1.0f * HP) / (1.0f * maxHP);
        Vector3 newHP = new Vector3( helthPercent, 1.0f, 1.0f );
        healthBar.transform.localScale = newHP;
    }

    public void SetWeaponName(string name) {
        weaponName.text = name;
    }
    public void SetRemainingAmmo(string ammo) {
        ammoRemaining.text = ammo;
    }
    public void AddToScore(int points) {
        score += points;
        scoreboard.text = score.ToString();
    }

    public void SetWave(int waveNum) {
        wave.text = "Wave " + waveNum.ToString();
    }
}