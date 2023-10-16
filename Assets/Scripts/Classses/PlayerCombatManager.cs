using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCombatManager : CombatManager
{
    public Camera fpsCamera;
    CharacterManager characterManager;
    PlayerInputManager inputManager;
    [HideInInspector] public CharacterAnimationManager animator;
    public GameObject damageNumberPrefab;
    public List<ItemList> items = new List<ItemList>();
    public new void Start() {
        StartCoroutine(CallItemUpdate());
        base.Start();
        Debug.Log("Initializing PlayerCombat Manager");
        inputManager = PlayerInputManager.Instance;
        characterManager = GetComponent<CharacterManager>();
        animator = GetComponent<CharacterAnimationManager>();
        

    }
    private new void Update() {
        // base.Update();
        HandleAllAttacks();
        Interact();
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

    public void CreateNumberPopUp(Vector3 position, string text, Color color) {
        var popup = Instantiate(damageNumberPrefab, position, quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = color;

        //Destroy Timer
        Destroy(popup, 1f);

        // Initialize objectPooling for damage numbers later on
    }

    IEnumerator CallItemUpdate() {
        foreach(ItemList i in items){
            i.item.Update(this, i.stacks);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(CallItemUpdate());
    }

    void Interact() {
        if (inputManager.hasPlayerInteracted()) {
            RaycastHit[] hits = Physics.RaycastAll(fpsCamera.transform.position, fpsCamera.transform.forward, 20f);
            foreach (RaycastHit hit in hits) {
                IInteract interactObject = hit.collider.GetComponent<IInteract>();
                Debug.Log("Looking for IInteractScript");
                if (interactObject != null) {
                    Debug.Log("Interacting");
                    interactObject.Interact();
                } else {
                    Debug.Log("Not interactable");
                }
            }
        }
        
    }
}
