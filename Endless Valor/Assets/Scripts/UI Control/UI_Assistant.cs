using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assistant : MonoBehaviour
{
    [SerializeField] private Statue parentStatue;
    [SerializeField] private TextMeshProUGUI messageTextPlace;
    [SerializeField] private float textWritingSpeed = 0.5f;
    [TextArea(8, 18)] [SerializeField] private string[] messageArray = new string[] { };
    
    private int messageTracker;
    private bool interactInput;
    private bool isFirstRun;
    
    //Bools
    private bool isWriterTriggered;


    public bool CheckIsLastMessage()
    {
        return messageTracker == messageArray.Length;
    }
    
    public TextMeshProUGUI GetTheTextPlaceHolder()
    {
        return messageTextPlace;
    }
    
    public void TriggerWriter()
    {
        isWriterTriggered = true;
    }
    
    private void OnEnable()
    {
        messageTracker = 0;
        isFirstRun = true;
    }


    private void Update()
    {
        if (Player.Instance != null)
        {
            interactInput = Player.Instance.InputHandler.InteractInput;
        }
        
        if (isWriterTriggered)
        {
            if (isFirstRun)
            {
                TextWriter.AddWriter_Static(messageTextPlace, messageArray[messageTracker], textWritingSpeed, true);
                
                messageTracker++;
                isWriterTriggered = false;
                isFirstRun = false;
            }
            else if (messageTextPlace.text == messageArray[messageTracker-1] && messageTracker <= messageArray.Length-1)
            {
                if (TextWriter.CheckIfActiveWriter_Static())
                {
                    TextWriter.RemoveWriter_Static(messageTextPlace);
                    messageTextPlace.text = "";
                }
                
                TextWriter.AddWriter_Static(messageTextPlace, messageArray[messageTracker], textWritingSpeed, true);
                
                messageTracker++;
                isWriterTriggered = false;
            }
            else if (messageTextPlace.text != messageArray[messageTracker-1] && messageTracker <= messageArray.Length)
            {
                messageTextPlace.text = messageArray[messageTracker-1];
                isWriterTriggered = false;
            }
            else if (messageTextPlace.text == messageArray[messageTracker - 1] && messageTracker == messageArray.Length)
            {
                messageTracker = 0;
                isFirstRun = true;
                isWriterTriggered = false;
                
                parentStatue.ImitateTriggerExit();
            }
            
        }
    }

    private void OnDisable()
    {
        TextWriter.RemoveWriter_Static(messageTextPlace);
        messageTextPlace.text = "";
    }
    
}
