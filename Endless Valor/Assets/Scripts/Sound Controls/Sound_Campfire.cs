using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Campfire : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private float minDist = 0.1f;
    private float maxDist = 9.0f;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float dist = Vector3.Distance(transform.position, other.transform.position);
            
            if(dist < minDist)
            {
                audioSource.volume = 1;
            }
            else if(dist > maxDist)
            {
                audioSource.volume = 0;
            }
            else
            {
                audioSource.volume = 1 - ((dist - minDist) / (maxDist - minDist));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Pause();
        }
    }
}
