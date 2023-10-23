using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour, IInteract
{
    [SerializeField] SpawnNewTerrain parentScript;
    public bool isOpen;
    public bool isWaveInAction;
    Vector3 positionOpen;
    Vector3 positionClosed;
    private void Start()
    {
        positionOpen = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        positionClosed = transform.position;
    }
    public void DoorHandle()
    {
        
        if (isOpen && !isWaveInAction)
        {
            transform.position = positionOpen;
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
        }
        else if(isOpen && isWaveInAction)
        {
            transform.position = positionClosed;
        }
    }
    public void doorOpen()
    {
        if (!isOpen)
        {
            parentScript.playerTrigger = true;
            isOpen = true;
            DoorHandle();
        }
    }

    public void Interact()
    {
        doorOpen();
    }

}
