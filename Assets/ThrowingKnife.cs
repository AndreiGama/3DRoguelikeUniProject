using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    public PlayerCombatManager playerCombatManager;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.GetMask("Enemy")) {
            IDamagable damagable = other.transform.GetComponent<IDamagable>();
            if(damagable != null) {
                damagable.doDamage(playerCombatManager.AbilityDamageCalculate(playerCombatManager.ability1Damage, true, other.name),  true, playerCombatManager);
            }
        }
    }
}
