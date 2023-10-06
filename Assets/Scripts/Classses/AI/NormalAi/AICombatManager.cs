using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICombatManager : CombatManager
{
    public NavMeshAgent agent;
    [SerializeField] GameObject player;
    [SerializeField] int AttackRange;
    [SerializeField] LayerMask playerLayer;
    bool isAttacking;

    private new void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        if (canAttackPlayer() && !isAttacking) {
            StartCoroutine(Attack());
        } else if(!isAttacking){
                Chase();
        }
    }

    bool canAttackPlayer() {
        if(Physics.CheckSphere(transform.position, AttackRange, playerLayer)) {
            return true;
        }
        else {
            return false;
        }
    }

    void Chase() {
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
    }

    IEnumerator Attack() {
        isAttacking= true;
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        if(canAttackPlayer()) {
            isAttacking = false;
            doDamage(weaponDamage());
            Debug.Log("Player is in range, do damage");
        }
        else { 
            isAttacking = false;
            Debug.Log("Player is not in range, back to chasing");
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, AttackRange);
    }
}
