using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    public CharacterController characterController;

    public bool isPerformingAction = false;
    public bool canMove = true;
    public bool canRotate = true;
    public bool applyRootMotion = false;
    public bool canLook = true;
    private void Awake() {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        characterController = GetComponent<CharacterController>() ;
    }
    private void Update() {
        
    }
}
