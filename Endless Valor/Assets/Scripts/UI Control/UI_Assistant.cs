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
    private bool isUpdatingMessages;
    
    //Bools
    private bool isWriterTriggered;

    public void TriggerMessagesUpdate() => isUpdatingMessages = true;
    
    private void ModifyMessage(int index, string message) //First message can't be modified else it will crash
    {
        if (index <= messageArray.Length - 1 && index != 0)
        {
            messageArray[index] = message;
        }
        else
            Debug.Log("Index out of array !!!");
    }
    
    public bool CheckIsLastMessage()
    {
        return (messageTracker == messageArray.Length && messageTextPlace.text == messageArray[messageTracker-1]);
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

                if (isUpdatingMessages)
                {
                    ModifyMessage(2, "Good Luck !" + Environment.NewLine + "[E] To Close");
                }
                
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
