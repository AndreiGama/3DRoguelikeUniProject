using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : CombatManager
{
    CharacterManager characterManager;
    PlayerInputManager inputManager;
    public CharacterAnimationManager animator;

    public void Start() {
        base.Start();
        Debug.Log("Initializing PlayerCombat Manager");
        animator = GetComponent<CharacterAnimationManager>();
        inputManager = PlayerInputManager.Instance;
        characterManager = GetComponent<CharacterManager>();

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
}
