using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Gunner : PlayerCombatManager {
    
    [SerializeField] GunWeaponData gunData;
    public float fireRate;  // Rate of fire in seconds
    float canFire;
    public float maxBulletHitRange;  // Maximum distance for the raycast
    public int maxTargetsPenetrate; // Maximum number of targets to hit

    //string PrimaryAttackAnimation = "HandgunShoot";
    //string SecondaryAttackAnimation = "HandGunTrippleShoot";

    string PrimaryAttackAnimationArms = "A_Arm_Fire";
    string PrimaryAttackAnimationWeapon = "A_Glock_Fire";

    //string ReloadAnimationArms = "A_Arm_Reload";
    //string ReloadAnimationWeapon = "A_Glock_Reload";

    //string SecondaryAttackAnimationArms;
    //string SecondaryAttackAnimationWeapon;
    [SerializeField] LayerMask layersToHit;

    [SerializeField] Recoil _recoil;

    [SerializeField] GameObject grenadeGameObject;
    [SerializeField] Transform projectileTransform;
    [SerializeField] GameObject VFX_BloodSplatter;

    [SerializeField] GameObject dynamitePrefab;
    [SerializeField] GameObject throwingKnifePrefab;
    private new void Start() {
        base.Start();
        fireRate = gunData.fireRate;
        maxBulletHitRange = gunData.maxBulletHitRange;
        maxTargetsPenetrate = gunData.maxTargetsPenetrate;
        StartCoroutine(canFireTimer());
        ability1Damage = 10;
        ability2Damage = 15;

        basePrimaryDamage *= difficultyDamage;
        ability1Damage *= difficultyDamage;
        ability2Damage *= difficultyDamage;
    }

    public void WeaponFire() {
        Debug.Log("WeaponFire");
            _recoil.recoil();

            RaycastHit[] hits = Physics.RaycastAll(fpsCamera.transform.position, fpsCamera.transform.forward, maxBulletHitRange, layersToHit, QueryTriggerInteraction.Collide);

            // Sort the hits by distance from the ray's origin in ascending order
            Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

            int penetratedTargets = 0; // Initialize a counter for penetrated targets
            Transform previousHitObject = null;

            foreach (RaycastHit hit in hits) {
                // Debug checking what objects it hit
                Debug.Log("Hit object: " + hit.collider.gameObject.name);

                
                Transform currentHitObject = hit.transform.root;
                if(previousHitObject != currentHitObject) {
                    // Grabs hit object and then grabs the damagable interface
                    GameObject hitObject = hit.transform.gameObject;
                    HitboxComponent hitbox = hitObject.GetComponent<HitboxComponent>();
                    IDamagable damagable = hitbox.parentIDamagable;
                    
                    // If the object hit has the damagable interface then do logic
                    if (damagable != null) {
                        // Increase the variable which counts how many targets it penetrated so far
                        penetratedTargets++;
                        Debug.Log("Damageable object hit, name is:" + hitObject.name);

                        // Apply damage and then create the damage text

                        //Grab hitbox component to check where the player hit
                        Debug.Log("Damage is being done");
                        int dmgAmount = PrimaryDamageCalculate(basePrimaryDamage, true, hitbox.bodyPartString);
                        damagable.doDamage(dmgAmount, true, this);
                        
                        CreateNumberPopUp(hitObject.transform.position, dmgAmount.ToString(), Color.white);
                        CreateBloodSplatter(hit);
                        previousHitObject = currentHitObject;

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

    void CreateBloodSplatter(RaycastHit hit) {
        Vector3 incomingVector = hit.point - transform.root.position;
        Vector3 reflectVector = Vector3.Reflect(incomingVector, hit.normal);

        GameObject temp = Instantiate(VFX_BloodSplatter, hit.point, Quaternion.Euler(reflectVector));
        GameObject.Destroy(temp, 1f);
    }
    IEnumerator canFireTimer() {
        canFire += Time.deltaTime;
        yield return new WaitForEndOfFrame();
        StartCoroutine(canFireTimer());
    }
    public override void PrimaryAttackLogic() {
        // Primary Attack Logic
        if (canFire > fireRate / attackSpeedAmplifier) {
            canFire = 0f;
            animator.PlayTargetActionAnimation(PrimaryAttackAnimationArms, PrimaryAttackAnimationWeapon, true, attackSpeedAmplifier);
        }
    }

    public override void SecondaryAttackLogic() {
        // Secondary Attack Logic
    }

    public override void Abillity1Logic() {
        GameObject throwKnife = Instantiate(throwingKnifePrefab, fpsCamera.transform.position, fpsCamera.transform.rotation);
        ThrowingKnife throwKnifeScript = throwKnife.GetComponentInChildren<ThrowingKnife>();
        throwKnifeScript.playerCombatManager = this;
        // First Abilitty Logic
    }

    public override void Abillity2Logic() {
        GameObject dynamite = Instantiate(dynamitePrefab, fpsCamera.transform.position, fpsCamera.transform.rotation);
        BombScript dynamiteScript = dynamite.GetComponentInChildren<BombScript>();
        dynamiteScript.playerCombatManager = this;
        // Second Abillity logic
    }
}
