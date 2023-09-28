using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCombatManager : CombatManager
{
    CharacterManager characterManager;
    PlayerInputManager inputManager;
    [HideInInspector] public CharacterAnimationManager animator;
    public GameObject damageNumberPrefab;

    public new void Start() {
        base.Start();
        Debug.Log("Initializing PlayerCombat Manager");
        inputManager = PlayerInputManager.Instance;
        characterManager = GetComponent<CharacterManager>();
        animator = GetComponent<CharacterAnimationManager>();

    }
    private void Update() {
        HandleAllAttacks();
    }

    void HandleAllAttacks() {
        //Primary Attack
        if (inputManager.hasPrimaryFireTriggered()) {
            PrimaryAttack();
        }
        //Secondary Attack
        if(inputManager.hasSecondaryFireTriggered()) {  
            SecondaryAttack();
        }
        //Ability1
        if (inputManager.hasAbility1Triggered()) {
            Abillity1();
        }
        //Ability2
        if (inputManager.hasAbility2Triggered()) { 
            Abillity2();
        }
        //Ultimate Ability
        if(inputManager.hasUltimateAbilityTriggered()) {
            UltimateAbility();
        }
        
    }
    public void PrimaryAttack() {
        if (!characterManager.isPerformingAction) {
            PrimaryAttackLogic();
        }
    }
    public virtual void PrimaryAttackLogic() {
        // Primary Attack Logic
    }
    public void SecondaryAttack() {
        if (!characterManager.isPerformingAction) {
            SecondaryAttackLogic();
        }
    }

    public virtual void SecondaryAttackLogic() {
        // Secondary Attack Logic
    }

    public void Abillity1() {
        if (!characterManager.isPerformingAction) {
            Abillity1Logic();
        }
    }

    public virtual void Abillity1Logic() {
        // First Abilitty Logic
    }

    public void Abillity2() {
        if (!characterManager.isPerformingAction) {
            Abillity2Logic();
        }
    }

    public virtual void Abillity2Logic() {
        // Second Abillity logic
    }

    public virtual void UltimateAbility() {
        if (!characterManager.isPerformingAction) {
            UltimateAbillityLogic();
        }
    }

    public virtual void UltimateAbillityLogic() {
        // Ultimate Abillity logic
    }

    public void CreateNumberPopUp(Vector3 position, string text, Color color) {
        var popup = Instantiate(damageNumberPrefab, position, quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = color;

        //Destroy Timer
        Destroy(popup, 1f);

        // Initialize objectPooling for damage numbers later on
    }
}
