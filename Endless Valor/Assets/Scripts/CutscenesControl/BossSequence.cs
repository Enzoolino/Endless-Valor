using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using CutscenesControl;
using UnityEngine;

public class BossSequence : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField] private Dialogue_Assistant bossDialogueAssistant;
    
    [Header("Camera Control")]
    [SerializeField] private float bossShowCameraShakeIntensity;
    [SerializeField] private float bossShowCameraShakeTime;

    private int dialoguePhase = 0;


    public void StartDialogueSequencing()
    {
        dialoguePhase = 0;
        bossDialogueAssistant.StartSubtitles(dialoguePhase);
    }

    public void GoToNextDialoguePhase()
    {
        dialoguePhase++;
        bossDialogueAssistant.StartSubtitles(dialoguePhase);
    }

    public void BossShowCameraShake()
    {
        CameraShake.Instance.ShakeCamera(bossShowCameraShakeIntensity, bossShowCameraShakeTime);
    }

    



}
