using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAnimationManager : MonoBehaviour {
    CharacterManager character;

    private void Start() {
        character = GetComponent<CharacterManager>();
    }
    public void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = true, bool canMove = true, bool canLook = true) {
        /* Default settings when the command gets run >
        string targetAnimation,
        bool isPerformingAction,
        bool applyRootMotion = true,
        bool canRotate = false,
        bool canMove = false
        canLook = true */

        character.animator.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);
        character.isPerformingAction = isPerformingAction;
        character.canMove = canMove;
        character.canRotate = canRotate;
        character.canLook = canLook;
    }
}