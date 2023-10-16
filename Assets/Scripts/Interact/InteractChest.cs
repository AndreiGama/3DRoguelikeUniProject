using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractChest : MonoBehaviour, IInteract
{
    [SerializeField] List<GameObject> crystals = new List<GameObject>();
    bool isUsed;
    public Transform spawnPos1, spawnPos2;

    public void Interact() {
        if (!isUsed) {
            isUsed = true;
            Instantiate(crystals[Random.Range(0, crystals.Count)], spawnPos1.position, spawnPos1.rotation);
            Instantiate(crystals[Random.Range(0, crystals.Count)], spawnPos2.position, spawnPos2.rotation);
        }
    }
}
