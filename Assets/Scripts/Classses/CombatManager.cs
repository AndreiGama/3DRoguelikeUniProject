using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour, IDamagable {
    public CharacterStats characterStats;
    public WeaponData weaponData;

    [HideInInspector] public string characterName;
    [HideInInspector] public int health;
    [HideInInspector] public int armor;
    [HideInInspector] public int movementSpeed;
    [HideInInspector] public int shield;
    [HideInInspector] public int maxHealth;
    [HideInInspector] public int maxShield;

    [HideInInspector] public string weaponName;
    [HideInInspector] public int baseDamage;
    [HideInInspector] public WeaponType weaponType;
    // Start is called before the first frame update
    public void Start() {
        Debug.Log("Initializing Stats");
        LoadBaseStats();
        LoadWeaponStats();

        maxHealth = health;
        maxShield = shield;
    }

    private void LoadWeaponStats() {
        weaponName = weaponData.weaponName;
        weaponType = weaponData.weaponType;
        baseDamage = weaponData.baseDamage;
    }

    private void LoadBaseStats() {
        characterName = characterStats.characterName;
        health = characterStats.health;
        armor = characterStats.armor;
        movementSpeed = characterStats.movementSpeed;
        shield = characterStats.shield;
    }

    public int weaponDamage() {
        return baseDamage;
    }
    public void doDamage(int dmgAmount) {
        if (shield > 0) {
            shield -= dmgAmount;

            // Check if there's still damage left after the shield is depleted
            if (shield < 0) {
                int remainingDamage = Mathf.Abs(shield);
                shield = 0;

                // Deduct the remaining damage from the health
                health -= remainingDamage;
            }
        } else {
            // If the shield is already depleted, deduct damage directly from health
            health -= dmgAmount;
        }
    }
}
