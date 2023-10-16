using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CombatManager : MonoBehaviour, IDamagable {
    [Header("Testing_References")] // remove when done verifying the core system work
    public CharacterStats characterStats;
    public WeaponData weaponData;

    [Header("Stats")]
    [Space(10)]
    [Header("CharacterStats")]
    [HideInInspector] public string characterName;
    [HideInInspector] public int baseHealth;
    public int health;
    [HideInInspector] public int baseArmor;
    public int armor;
    [HideInInspector] public int baseMovementSpeed;
    public int movementSpeed;
    [HideInInspector] public int baseShield;
    public int shield;
    [HideInInspector] public int baseMaxHealth;
    public int maxHealth;
    [HideInInspector] public int baseMaxShield;
    public int maxShield;
    [Space(5)]
    [Header("WeaponStats")]
    [HideInInspector] public string weaponName;
    [HideInInspector] public int basePrimaryDamage;
    [HideInInspector] public WeaponType weaponType;
    [Space(10)]
    [Header("Amplifiers")]
    public float abilityDamageAmplifier = 1f;
    public float primaryDamageAmplifier = 1f;
    public float healingAmplifier = 1f;
    public float movementSpeedAmplifier = 1f;
    public float attackSpeedAmplifier = 1f;
    public float shieldAmplifier = 1f;
    public float armorAmplifier = 1f;
    public float healthAmplifier = 1f;
    // Start is called before the first frame update
    public void Start() {
        Debug.Log("Initializing Stats");
        LoadBaseStats();
        LoadWeaponStats();
        baseMaxHealth = health;
        baseMaxShield = baseShield;
    }
    public void AmplifyStats() {
        armor = Mathf.FloorToInt(baseArmor * armorAmplifier);
        movementSpeed = Mathf.FloorToInt(baseMovementSpeed * movementSpeedAmplifier);
        maxShield = Mathf.FloorToInt(baseMaxShield * shieldAmplifier);
        maxHealth = Mathf.FloorToInt(baseMaxHealth * armorAmplifier);
    }

    public int AbilityDamageCalculate(int AbilityDamage) {
        return Mathf.FloorToInt(AbilityDamage * abilityDamageAmplifier);
    }

    public int PrimaryDamageCalculate(int BaseDamage) {
        return Mathf.FloorToInt(BaseDamage * primaryDamageAmplifier);
    }

    public void Heal(int healthToAdd) {
        health = Mathf.Clamp(healthToAdd, health, maxHealth);
    }
    private void LoadWeaponStats() {
        weaponName = weaponData.weaponName;
        weaponType = weaponData.weaponType;
        basePrimaryDamage = weaponData.baseDamage;
    }

    private void LoadBaseStats() {
        characterName = characterStats.characterName;
        baseHealth = characterStats.health;
        baseArmor = characterStats.armor;
        baseMovementSpeed = characterStats.movementSpeed;
        baseShield = characterStats.shield;

        health = baseHealth;
        armor = baseArmor;
        movementSpeed = baseMovementSpeed;
        shield = baseShield;
    }
    public void doDamage(int dmgAmount) {
        if (baseShield > 0) {
            baseShield -= dmgAmount;

            // Check if there's still damage left after the shield is depleted
            if (baseShield < 0) {
                int remainingDamage = Mathf.Abs(baseShield);
                baseShield = 0;

                // Deduct the remaining damage from the health
                health -= remainingDamage;
            }
        } else {
            // If the shield is already depleted, deduct damage directly from health
            health -= dmgAmount;
        }
    }

    public void die() {
        if(health <= 0) {
            Debug.Log("Die");
            Destroy(gameObject);
        }
    }
    public void Update() {
        die();
    }
}
