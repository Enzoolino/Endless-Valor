using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject enabledSoundButton;
    [SerializeField] private GameObject disabledSoundButton;
    
    
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EnableSound()
    {
        musicSource.mute = false;
        enabledSoundButton.SetActive(true);
        disabledSoundButton.SetActive(false);
    }
    
    public void MuteSound()
    {
        musicSource.mute = true;
        enabledSoundButton.SetActive(false);
        disabledSoundButton.SetActive(true);
    }
    
    

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
