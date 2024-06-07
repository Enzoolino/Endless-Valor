using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProximityInteractText : MonoBehaviour
{
    private TextMeshProUGUI overheadText;
    private bool playerInsideTrigger;

    //TODO: Make it into singleton
    [SerializeField] private TextFadeEffect fadeEffect;

    private void Start()
    {
        overheadText = GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            overheadText.enabled = true;
            playerInsideTrigger = true;
            fadeEffect.FadeTextIn(overheadText);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fadeEffect.FadeTextOut(overheadText);
            playerInsideTrigger = false;
            StartCoroutine(WaitForExit());
        }
    }
    
    
    IEnumerator WaitForExit()
    {
        yield return new WaitForSeconds(5);

        if (!playerInsideTrigger)
        {
            overheadText.enabled = false;
        }
    }
}
