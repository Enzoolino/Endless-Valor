using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    private bool interactInput;
    
    [SerializeField] private ProximityInteractText interactText;
    [SerializeField] private ProximityInteractText temporaryText;

    private void OnTriggerStay(Collider other)
    {
        if (Player.Instance != null)
        {
            interactInput = Player.Instance.InputHandler.InteractInput;
        }
        
        if (other.CompareTag("Player") && interactInput)
        {
            interactText.enabled = false;
            temporaryText.enabled = true;
        }
        
        
    }
}
