using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Campfire : MonoBehaviour
{
    [SerializeField] private AudioSource audioSound;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSound.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSound.Pause();
        }
    }
}
