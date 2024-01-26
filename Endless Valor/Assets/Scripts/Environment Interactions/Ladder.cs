using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Player.Instance != null)
            {
                Debug.Log("Ladder detected");
                Player.Instance.isNearLadder = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Player.Instance != null)
            {
                Player.Instance.isNearLadder = false;
            }
        }
    }
}
