using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public CharacterStats characterStats;
    public WeaponData weaponData;

    public string characterName;
    public int health;
    public int armor;
    public int movementSpeed;
    public int shield;

    public string weaponName;
    public int baseDamage;
    public WeaponType weaponType;
    // Start is called before the first frame update
    public void Start() {
        Debug.Log("Initializing Stats");
        characterName = characterStats.characterName;
        health = characterStats.health;
        armor = characterStats.armor;
        movementSpeed = characterStats.movementSpeed;
        shield = characterStats.shield;

        weaponName = weaponData.weaponName;
        weaponType = weaponData.weaponType;
        baseDamage = weaponData.baseDamage;
    }

    
}
