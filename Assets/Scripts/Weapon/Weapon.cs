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
    [SerializeField] ParticleSystem defaultHitEffectVFX;
    public string weaponName;
    private Ammo ammo;
    private Camera FPSCamera;
    private bool isReloading = false;
    private Animator animator;

    void Start() {
        FPSCamera = Camera.main;
        ammo = GetComponentInParent<Ammo>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetAxis("Fire1") == 1) {                 
                ProcessFiring();
        }
    }

    void OnEnable() {
        if (isReloading) {
            StartCoroutine(Reload());
        }
    }

    void ProcessFiring() {
        if (ammo.GetAmmoAmount(ammoType) > 0) {
            if (animator != null) {                
                if ( !animator.GetBool("shoot") && !isReloading ) {
                    animator.SetBool("shoot",true);
                }
            } else {
                Shoot();
            }
        }
    }

    private void Shoot() {
        if (isReloading) { return; }
        ProcessRaycast();
        ammo.DecreaseAmmo(ammoType,ammoPerShot);
        StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(timeBetweenShots);
        if (animator != null) {
            animator.SetBool("shoot",false);
        }
        isReloading = false;
    }

    private void ProcessRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit,weaponRange)) {            
            Headshot headshot = hit.transform.GetComponent<Headshot>();
            Enemy enemy = hit.transform.GetComponentInParent<Enemy>();      
            if (headshot) {
                int headshotDamage = weaponDamage * headshot.GetHeadshotMultiplier();
                ProcessEnemyHit(enemy, hit, headshotDamage);
            }else if (enemy) { 
                ProcessEnemyHit(enemy, hit, weaponDamage);
            } else {
                PlayDefaultHitEffect(hit.point);
            }                    
        }
    }

    private void ProcessEnemyHit(Enemy enemy, RaycastHit hit, int dmg) {
        enemy.InflictDamage(dmg);
        ParticleSystem enemyHitEffect = enemy.GetHitEffect();
        float hitEffectDuration = enemyHitEffect.main.duration;
        Destroy( Instantiate<ParticleSystem>(enemyHitEffect,hit.point,Quaternion.identity,enemy.transform).gameObject, hitEffectDuration);
    }

    private void PlayDefaultHitEffect(Vector3 location) {
        float hitEffectDuration = defaultHitEffectVFX.main.duration;
        Destroy( Instantiate<ParticleSystem>(defaultHitEffectVFX,location,Quaternion.identity).gameObject, hitEffectDuration);
    }
}
