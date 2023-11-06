using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour, IInteract
{
    GameManager gameManager;   
    public bool isOpen;
    Vector3 positionOpen;
    Vector3 positionClosed;
    Collider gateCollider;
    private void Start()
    {
        gateCollider = GetComponent<Collider>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>() ;
        positionOpen = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        positionClosed = transform.position;
        StartCoroutine(DoorManager());
    }


    IEnumerator DoorManager() {
        yield return new WaitForSeconds(.5f);
        if (gameManager.isInWave) {
            DoorClose();
        } else if (!gameManager.isInWave && isOpen) {
            doorOpen();
        }
        StartCoroutine(DoorManager());
    }
    public void doorOpen()
    {
        transform.position = positionOpen;
        gateCollider.enabled = false;
    }
    public void DoorClose() {
        transform.position = positionClosed;
        gateCollider.enabled = true;
    }

    public void Interact(PlayerCombatManager player = null)
    {
        if (!isOpen) {
            isOpen = true;
            doorOpen();
        }

    }

}
