using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenManager : MonoBehaviour
{
    public void Retry()
    {
        if (GameManager.Instance != null)
        {
            Debug.Log("Retrying the game");
            GameManager.Instance.StartNewGame();
        }
            
    }

    public void MainMenu()
    {
        if (GameManager.Instance != null)
        {
            Debug.Log("Returning to main menu");
            GameManager.Instance.ReturnToMainMenu();
        }
            
    }
    
    
}
