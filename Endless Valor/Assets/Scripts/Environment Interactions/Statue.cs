using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Statue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI overheadTextInitial;
    [SerializeField] private UI_Assistant assignedAssistant;
    [SerializeField] private TextFadeEffect fadeEffect;
    
    private bool interactInput;


    public void ImitateTriggerExit()
    {
        overheadTextInitial.enabled = true;
        assignedAssistant.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            overheadTextInitial.enabled = true;
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
            
            assignedAssistant.enabled = false;
        }
    }
}
