using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5.0f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position,target.position);

        if (isProvoked) { EngageTarget(); }
        else if (distanceToTarget <= chaseRange) { isProvoked = true; }       
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,chaseRange);
    }

    private void EngageTarget() {
        if (distanceToTarget <= navMeshAgent.stoppingDistance) { AttackTarget(); }
        else { ChaseTarget(); }
    }

    private void AttackTarget() {
        Debug.Log("Attack!");
    }

    private void ChaseTarget() {
        navMeshAgent.SetDestination(target.position);
    }
}
