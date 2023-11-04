using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    [SerializeField]LayerMask enemyLayer;
    public void CallItem(int damage, PlayerCombatManager player) {

        Collider[] collisions = Physics.OverlapSphere(transform.position, 3f, enemyLayer, QueryTriggerInteraction.Collide);
        foreach(Collider collision in collisions) {
            IDamagable damagable = collision.GetComponent<IDamagable>();
            if(damagable != null) {
                damagable.doDamage(damage, true, player);
            }
        }
    }
}
