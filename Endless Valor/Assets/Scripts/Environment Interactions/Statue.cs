using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Statue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI overheadTextInitial;
    [SerializeField] private UI_Assistant assignedAssistant;
    [SerializeField] private Light2D statueLight;
    [SerializeField] private float maxStatueLightIntensity = 1.0f;
    [SerializeField] private float intensityIncreaseValue = 0.1f;
    
    //TODO: Make it into singleton
    [SerializeField] private TextFadeEffect fadeEffect;
    
    //Bools
    private bool interactInput;
    private bool playerInsideTrigger;
    private bool enableStatueLight;


    private void Update()
    {
        if (enableStatueLight)
        {
            if (statueLight.intensity < 1)
            {
                IncreaseStatueLight();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            overheadTextInitial.enabled = true;
            playerInsideTrigger = true;
            fadeEffect.FadeTextIn(overheadTextInitial);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Player.Instance != null)
        {
            interactInput = Player.Instance.InputHandler.InteractInput;
        }
        
        if (other.CompareTag("Player") && interactInput)
        {
            overheadTextInitial.enabled = false;
            Debug.Log("Overhead text disabled");
            
            assignedAssistant.enabled = true;
            Debug.Log("UI Assistant enabled");
            
            assignedAssistant.TriggerWriter();
            Player.Instance.InputHandler.UseInteractInput();

            enableStatueLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (overheadTextInitial.enabled)
                fadeEffect.FadeTextOut(overheadTextInitial);
            else if (assignedAssistant.enabled)
                fadeEffect.FadeTextOut(assignedAssistant.GetTheTextPlaceHolder());

            playerInsideTrigger = false;

            StartCoroutine(WaitAfterExit());
        }
    }

    IEnumerator WaitAfterExit()
    {
        yield return new WaitForSeconds(5);

        if (!playerInsideTrigger)
        {
            overheadTextInitial.enabled = false;
            assignedAssistant.enabled = false;
        }
    }
    
    public void ImitateTriggerExit()
    {
        overheadTextInitial.enabled = true; //Player is still inside trigger so it can't be disabled here
        assignedAssistant.enabled = false;
    }
    
    private void IncreaseStatueLight()
    {
        statueLight.intensity += intensityIncreaseValue * Time.deltaTime;
    }
}
