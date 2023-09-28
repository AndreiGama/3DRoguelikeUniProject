using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerClass {

}
public class CharacterCameraController : MonoBehaviour {
    PlayerInputManager inputManager;
    [SerializeField]
    CharacterManager characterManager;

    [SerializeField] Transform playerBody;
    [SerializeField] Transform headCamera;
    Vector2 lookInput;
    Vector2 cameraWantedRotation;
    public float Sensitivity;
    float xRotation = 0f;
    private void Start() {
        inputManager = PlayerInputManager.Instance;
        characterManager = GetComponent<CharacterManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void HandleCameraMovement() {
        if (characterManager.canLook) {
            lookInput = inputManager.GetMouseDelta();
            float lookX = lookInput.x * Sensitivity * Time.deltaTime;
            float lookY = lookInput.y * Sensitivity * Time.deltaTime;
            xRotation -= lookY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            headCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * lookX);
        }
    }
    private void Update() {
        HandleCameraMovement();
    }
}