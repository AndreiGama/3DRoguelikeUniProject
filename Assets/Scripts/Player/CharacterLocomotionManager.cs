using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLocomotionManager : MonoBehaviour {
    Rigidbody rb;
    PlayerInputManager inputManager;
    CharacterManager characterManager;
    CombatManager combatManager;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        inputManager = PlayerInputManager.Instance;
        characterManager = GetComponent<CharacterManager>();
        combatManager = GetComponent<CombatManager>();
    }

    private void FixedUpdate() {
        HandleAllMovement();
    }

    public void HandleAllMovement() {
        HandleGroundedMovement();
    }
    
    void HandleGroundedMovement() {
        if (characterManager.canMove) {
            Vector2 playerInput = inputManager.GetPlayerMovement();
            Vector3 Move = transform.right * playerInput.x + transform.forward * playerInput.y;
            characterManager.characterController.Move(Move * combatManager.movementSpeed * Time.deltaTime);
        }
    }
}