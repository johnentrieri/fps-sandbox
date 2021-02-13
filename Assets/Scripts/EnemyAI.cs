using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10.0f;
    [SerializeField] float disengageDistance = 30.0f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    public bool isProvoked = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Idle();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position,target.position);
        Debug.Log(distanceToTarget);

        if (isProvoked) { EngageTarget(); }
        else if (distanceToTarget <= chaseRange) { isProvoked = true; }
        else { Idle(); }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,chaseRange);
    }

    private void EngageTarget() {
        if (distanceToTarget <= navMeshAgent.stoppingDistance) { AttackTarget(); }
        else if (distanceToTarget >= disengageDistance) { Idle(); }
        else { ChaseTarget(); }
    }

    private void AttackTarget() {
        animator.SetBool("attack",true);
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
}
