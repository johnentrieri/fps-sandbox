using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour
{
    [SerializeField] float weaponRange = 100.0f;
    [SerializeField] int weaponDamage = 1;
    [SerializeField] float timeBetweenShots = 0.2f;
    [SerializeField] int ammoPerShot = 1;
    [SerializeField] AmmoType ammoType;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] ParticleSystem defaultHitEffectVFX;
    private Ammo ammo;
    private Camera FPSCamera;
    private bool isReloading = false;

    void Start() {
        FPSCamera = Camera.main;
        ammo = GetComponentInParent<Ammo>();
    }

    void Update()
    {
        if (Input.GetAxis("Fire1") == 1) {
            if (ammo.GetAmmoAmount(ammoType) > 0) {
                Shoot();
            }
        }
    }

    void OnEnable() {
        if (isReloading) {
            StartCoroutine(Reload());
        }
    }

    private void Shoot() {
        if (isReloading) { return; }
        PlayMuzzleFlash();
        ProcessRaycast();
        ammo.SubtractAmmo(ammoType,ammoPerShot);
        StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(timeBetweenShots);
        isReloading = false;
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
