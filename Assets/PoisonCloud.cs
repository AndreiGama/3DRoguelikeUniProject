using Unity.VisualScripting;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    [SerializeField] LayerMask enemy;
    Transform lastHitTransform;
    public void CallItem( int damage, PlayerCombatManager player) {
        RaycastHit[] collisions = Physics.SphereCastAll(transform.position, 3f, Vector3.forward, 3f, enemy, QueryTriggerInteraction.Collide);
        
        foreach (RaycastHit collision in collisions) {
            if (lastHitTransform != collision.transform.root) {
                IDamagable damagable = collision.transform.GetComponentInParent<IDamagable>();
                if (damagable != null) {
                    damagable.doDamage(damage, true, player);
                    player.CreateNumberPopUp(collision.transform.position, damage.ToString(), Color.magenta);
                }
                lastHitTransform = collision.transform.root;
            }
                
        }
    }
}
