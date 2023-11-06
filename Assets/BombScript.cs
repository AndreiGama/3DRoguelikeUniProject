using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    public PlayerCombatManager playerCombatManager;
    int durationToExplode;
    private void Start() {
        Invoke("Explode", durationToExplode);
    }
    void Explode() {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 3f, Vector3.zero, 3f, LayerMask.GetMask("Enemy"), QueryTriggerInteraction.Collide);
        
        foreach (RaycastHit hit in hits) {
            IDamagable damagable = hit.collider.GetComponent<IDamagable>();
            if (damagable != null) {
                damagable.doDamage(playerCombatManager.AbilityDamageCalculate(playerCombatManager.ability2Damage), true, playerCombatManager);
            }
        }
    }
}
