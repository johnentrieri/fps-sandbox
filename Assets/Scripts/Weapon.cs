using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour
{
    [SerializeField] float weaponRange = 100.0f;
    [SerializeField] int weaponDamage = 1;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] ParticleSystem defaultHitEffectVFX;
    [SerializeField] Ammo ammo;
    private Camera FPSCamera;

    void Start() {
        FPSCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            if (ammo.GetAmmoAmount() > 0) {
                Shoot();
            }
        };
    }

    private void Shoot() {
        PlayMuzzleFlash();
        ProcessRaycast();
        ammo.SubtractAmmo(1);
    }

    private void ProcessRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit,weaponRange)) {            
            
            EnemyHealth enemyHealth = hit.transform.GetComponentInParent<EnemyHealth>();      
            if (enemyHealth) { 
                ProcessEnemyHit(enemyHealth, hit);
            } else {
                PlayDefaultHitEffect(hit.point);
            }                    
        }
    }

    private void ProcessEnemyHit(EnemyHealth enemyHealth, RaycastHit hit) {
        enemyHealth.InflictDamage(weaponDamage);
        ParticleSystem enemyHitEffect = enemyHealth.GetHitEffect();
        float hitEffectDuration = enemyHitEffect.main.duration;
        Destroy( Instantiate<ParticleSystem>(enemyHitEffect,hit.point,Quaternion.identity,enemyHealth.transform).gameObject, hitEffectDuration);
    }

    private void PlayMuzzleFlash() {
        muzzleFlashVFX.Play();
    }

    private void PlayDefaultHitEffect(Vector3 location) {
        float hitEffectDuration = defaultHitEffectVFX.main.duration;
        Destroy( Instantiate<ParticleSystem>(defaultHitEffectVFX,location,Quaternion.identity).gameObject, hitEffectDuration);
    }
}
