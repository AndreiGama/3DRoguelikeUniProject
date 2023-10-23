using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGateTrigger : MonoBehaviour
{
    // Reference to game manager
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get reference to game manager
            // Set new wave
            // Close Door
            // Make sure to not let the player spawn multiple waves or use 
        }
    }
}
