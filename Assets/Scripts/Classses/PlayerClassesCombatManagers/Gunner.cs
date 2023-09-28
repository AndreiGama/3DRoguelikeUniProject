using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gunner : PlayerCombatManager {
    public Camera fpsCamera;
    [SerializeField] GunWeaponData gunData;
    public float fireRate;  // Rate of fire in seconds
    private float nextFireTime;  // Time when the next shot can be fired
    public float maxBulletHitRange;  // Maximum distance for the raycast
    public int maxTargetsPenetrate; // Maximum number of targets to hit

    private int targetsHit = 0; // Counter for the number of targets hit

    string PrimaryAttackAnimation = "HandgunShoot";
    string SecondaryAttackAnimation = "HandGunTrippleShoot";
    [SerializeField] LayerMask layersToHit;
    private new void Start() {
        base.Start();
        fireRate = gunData.fireRate;
        nextFireTime = gunData.nextFireTime;
        maxBulletHitRange = gunData.maxBulletHitRange;
        maxTargetsPenetrate = gunData.maxTargetsPenetrate;
    }

    public void WeaponFire() {
        targetsHit = 0;
        Debug.Log("WeaponFire");
        if (Time.time > nextFireTime) {
            // Set the next time a shot can be fired
            nextFireTime = Time.time + fireRate;
           
            RaycastHit[] hits = Physics.RaycastAll(fpsCamera.transform.position, fpsCamera.transform.forward, maxBulletHitRange, layersToHit);

            foreach (RaycastHit hit in hits) {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                if (targetsHit > maxTargetsPenetrate) {
                    Debug.Log("Breaking out of for each loop");
                    // If we've hit the maximum number of targets, break out of the loop
                    break;
                }
                GameObject hitObject = hit.collider.gameObject;
                IDamagable damagable = hitObject.GetComponent<IDamagable>();
                if(damagable != null) {
                    Debug.Log("Damageable object hit, name is:" + hitObject.name);
                    damagable.doDamage(weaponDamage());
                    CreateNumberPopUp(hitObject.transform.position, "" + weaponDamage(), Color.white);
                    targetsHit++;
                }
                else if(damagable == null) {
                    Debug.Log("No damagable objects found: " + hitObject.name);
                    break;
                }
                // Increment the targets hit counter
            }
        }
    }
    public override void PrimaryAttackLogic() {
        // Primary Attack Logic
        animator.PlayTargetActionAnimation(PrimaryAttackAnimation, true);
    }

    public override void SecondaryAttackLogic() {
        // Secondary Attack Logic
        animator.PlayTargetActionAnimation(SecondaryAttackAnimation, true);
    }

    public override void Abillity1Logic() {
        // First Abilitty Logic
    }

    public override void Abillity2Logic() {
        // Second Abillity logic
    }

    public override void UltimateAbillityLogic() {
        // Ultimate Abillity logic
    }
}
