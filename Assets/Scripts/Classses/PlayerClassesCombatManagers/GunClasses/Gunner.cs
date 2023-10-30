using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Gunner : PlayerCombatManager {
    
    [SerializeField] GunWeaponData gunData;
    public float fireRate;  // Rate of fire in seconds
    private float nextFireTime;  // Time when the next shot can be fired
    public float maxBulletHitRange;  // Maximum distance for the raycast
    public int maxTargetsPenetrate; // Maximum number of targets to hit

    string PrimaryAttackAnimation = "HandgunShoot";
    string SecondaryAttackAnimation = "HandGunTrippleShoot";

    string PrimaryAttackAnimationArms = "A_Arm_Fire";
    string PrimaryAttackAnimationWeapon = "A_Glock_Fire";

    string ReloadAnimationArms = "A_Arm_Reload";
    string ReloadAnimationWeapon = "A_Glock_Reload";

    string SecondaryAttackAnimationArms;
    string SecondaryAttackAnimationWeapon;
    [SerializeField] LayerMask layersToHit;

    [SerializeField] Recoil _recoil;

    [SerializeField] GameObject grenadeGameObject;
    [SerializeField] Transform projectileTransform;
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
            _recoil.recoil();

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
                    damagable.doDamage(basePrimaryDamage);
                    CreateNumberPopUp(hitObject.transform.position, "" + basePrimaryDamage, Color.white);

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

    //IEnumerator recoil() {
    //    Vector3 ogCameraPosition = fpsCamera.transform.localRotation.eulerAngles;
    //    Vector3 recoiledCamera = new Vector3(fpsCamera.transform.localRotation.eulerAngles.x - recoilAmount, fpsCamera.transform.localRotation.eulerAngles.y, fpsCamera.transform.localRotation.eulerAngles.z);
    //    //= Quaternion.Euler(Vector3.Lerp(fpsCamera.transform.localRotation.eulerAngles, recoiledCamera, RecoilTime));

    //    float counter = 0;
    //    while ( counter < RecoilTime)
    //    {
    //        counter += Time.deltaTime;
    //        fpsCamera.transform.localEulerAngles = Vector3.Lerp(recoiledCamera, ogCameraPosition, counter / RecoilTime);

    //        yield return new WaitForEndOfFrame();

    //    }
    //    while (counter >= 0) {
    //        counter -= Time.deltaTime;
    //        fpsCamera.transform.localEulerAngles = Vector3.Lerp(recoiledCamera, ogCameraPosition, counter / RecoilTime);

    //        yield return new WaitForEndOfFrame();

    //    }

    //}
    

    
    public override void PrimaryAttackLogic() {
        // Primary Attack Logic
        animator.PlayTargetActionAnimation(PrimaryAttackAnimationArms, PrimaryAttackAnimationWeapon, true, attackSpeedAmplifier);
    }

    public override void SecondaryAttackLogic() {
        Instantiate(grenadeGameObject, projectileTransform.position, fpsCamera.transform.rotation);
        // Secondary Attack Logic
    }

    public override void Abillity1Logic() {
        // First Abilitty Logic
        GameManager gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        gameManager.isInWave = true;
    }

    public override void Abillity2Logic() {
        // Second Abillity logic
        GameManager gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        gameManager.isInWave = false;
    }
}
