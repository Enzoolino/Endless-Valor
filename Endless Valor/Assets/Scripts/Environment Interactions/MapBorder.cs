using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.isDead = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Instant death - reached border of the map
            Player.Instance.InstaDead();
        }
    }
}
