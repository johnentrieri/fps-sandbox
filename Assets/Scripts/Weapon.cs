using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] float weaponRange = 100.0f;
    [SerializeField] int weaponDamage = 1;
    [SerializeField] ParticleSystem muzzleFlashVFX;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        };
    }

    private void Shoot() {
        PlayMuzzleFlash();
        ProcessRaycast();
    }

    private void ProcessRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit,weaponRange)) {            

            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            EnemyAI enemyAI = hit.transform.GetComponent<EnemyAI>();

            if (enemyHealth) { enemyHealth.InflictDamage(weaponDamage); }
            if (enemyAI) { enemyAI.isProvoked = true; }
        }
    }

    private void PlayMuzzleFlash() {
        muzzleFlashVFX.Play();
    }
}
