using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLightBall : MonoBehaviour
{
    [SerializeField] private GameObject lightBall;
    [SerializeField] UI_Assistant uiAssistant;
    
    private void Update()
    {
        if (TextWriter.CheckIfActiveWriter_Static() && uiAssistant.CheckIsLastMessage())
        {
            lightBall.SetActive(true);
            uiAssistant.TriggerMessagesUpdate();
            enabled = false;
        }
    }
}
