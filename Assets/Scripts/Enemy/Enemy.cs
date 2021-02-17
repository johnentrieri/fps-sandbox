using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int HP = 10;
    [SerializeField] float chaseRange = 10.0f;
    [SerializeField] float turnSpeed = 30.0f;
    [SerializeField] float disengageDistance = 30.0f;
    [SerializeField] int enemyDamage = 1;
    [SerializeField] ParticleSystem enemyHitEffect;
    [SerializeField] ParticleSystem deathEffect;
    private NavMeshAgent navMeshAgent;
    private Collider enemyCollider;
    float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;
    private Animator animator;
    private Transform target;
    private bool isAlive = true;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().transform.parent.transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyCollider = GetComponent<Collider>();
        Idle();
    }

    void Update()
    {
        if (!isAlive) { return; }
        distanceToTarget = Vector3.Distance(transform.position,target.position);

        if (isProvoked) { EngageTarget(); }
        else if (distanceToTarget <= chaseRange) { isProvoked = true; }
        else { Idle(); }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,chaseRange);
    }
    public ParticleSystem GetHitEffect() {
        return enemyHitEffect;
    }

    public void InflictDamage(int dmg) {
        if (!isAlive) { return; }
        HP -= dmg;
        isProvoked = true;

        if (HP <= 0) {
            HP = 0;
            ProcessDeath();
        }
    }

    private void ProcessDeath() {
        if (!isAlive) { return; }      
        animator.SetTrigger("die");
        GetComponentInParent<EnemyManager>().EnemyDeathHandler();
        isAlive = false;
        navMeshAgent.enabled = false;
        enemyCollider.enabled = false;
    }

    private void EngageTarget() {
        if (!isAlive) { return; }
        FaceTarget();
        if (distanceToTarget <= navMeshAgent.stoppingDistance) { AttackTarget(); }
        else if (distanceToTarget >= disengageDistance) { Idle(); }
        else { ChaseTarget(); }
    }

    private void AttackTarget() {
        animator.SetBool("attack",true);
    }

    private void FaceTarget () {
        if (!isAlive) { return; }
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDirection.x,0,targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void ChaseTarget() {
        animator.SetBool("attack",false);
        animator.SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void Idle() {
        isProvoked = false;
        navMeshAgent.SetDestination(transform.position);
        animator.SetTrigger("idle");    
    }

    private void DamagePlayer() {
        target.GetComponentInChildren<PlayerHealth>().InflictDamage(enemyDamage);
    }

}
