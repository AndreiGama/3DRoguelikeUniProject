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
        Debug.Log("WeaponFire");
        if (Time.time > nextFireTime) {
            RaycastHit[] hits = Physics.RaycastAll(fpsCamera.transform.position, fpsCamera.transform.forward, maxBulletHitRange, layersToHit);

            // Sort the hits by distance from the ray's origin in ascending order
            Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

            int penetratedTargets = 0; // Initialize a counter for penetrated targets

            foreach (RaycastHit hit in hits) {
                // Debug checking what objects it hit
                Debug.Log("Hit object: " + hit.collider.gameObject.name);

                // Grabs hit object and then grabs the damagable interface
                GameObject hitObject = hit.collider.gameObject;
                IDamagable damagable = hitObject.GetComponent<IDamagable>();

                // If the object hit has the damagable interface then do logic
                if (damagable != null) {
                    // Increase the variable which counts how many targets it penetrated so far
                    penetratedTargets++;
                    Debug.Log("Damageable object hit, name is:" + hitObject.name);

                    // Apply damage and then create the damage text
                    damagable.doDamage(weaponDamage());
                    CreateNumberPopUp(hitObject.transform.position, "" + weaponDamage(), Color.white);

                    // If we've hit the maximum number of targets, break out of the loop
                    if (penetratedTargets >= maxTargetsPenetrate) {
                        Debug.Log("Reached max penetrated targets, breaking out of loop");
                        break;
                    }
                } else {
                    // If the object hit has no damagable interface then continue to the next target
                    continue;
                }
            }

        }
    }
    public override void PrimaryAttackLogic() {
        // Primary Attack Logic
        animator.PlayTargetActionAnimation(PrimaryAttackAnimation, true);
        WeaponFire();
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
