using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLocomotionManager : MonoBehaviour {
    // Grabs input manager so the script can know what the player pressed
    PlayerInputManager inputManager;
    // Grabs the charactermanager to check for certain conditions like if the player canmove or not
    CharacterManager characterManager;
    // Grabs the playercombatmanager script to grab base stats such as movement speed
    PlayerCombatManager combatManager;
    //Physics
    public float gravity = -9.81f;
    Vector3 velocity;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask; // Which layer to check for ground
    public float jumpHeight = 3f;


    private void Start() {
        inputManager = PlayerInputManager.Instance;
        characterManager = GetComponent<CharacterManager>();
        combatManager = GetComponent<PlayerCombatManager>();
    }

    private void FixedUpdate() {
        HandleGroundedMovement();
        Jump(); // Jump Function
    }
    
    void HandleGroundedMovement() {
        if (characterManager.canMove) {
            //Resets velocity.y when grounded
            if (isGrounded() && velocity.y < 0) {
                velocity.y = -2f;
            }
            // grabs imput from player, then moves
            Vector2 playerInput = inputManager.GetPlayerMovement();
            Vector3 Move = transform.right * playerInput.x + transform.forward * playerInput.y;
            characterManager.characterController.Move(Move * combatManager.movementSpeed * Time.deltaTime);
            // Handle gravity
            velocity.y += gravity + Time.deltaTime;
            characterManager.characterController.Move(velocity * Time.deltaTime);
        }
    }
    
    //Checks if the sphere that got created at the players feet hits the ground or not
    public bool isGrounded() {
        return Physics.CheckSphere(groundCheck.position, groundDistance);
    }

    void Jump() {
        
        if (inputManager.hasPlayerJumped() && isGrounded()) {
            Debug.Log(inputManager.hasPlayerJumped());
            Debug.Log(isGrounded());
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    
}