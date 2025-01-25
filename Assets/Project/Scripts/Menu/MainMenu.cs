using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainArea;
    public GameObject OptionsArea;
    public AudioSource ClickSound;

    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
        ClickSound.Play();
    }
    public void ShowOptions()
    {
        MainArea.SetActive(false);
        OptionsArea.SetActive(true);
        ClickSound.Play();
    }

    public void HideOptions()
    {
        MainArea.SetActive(true);
        OptionsArea.SetActive(false);
        ClickSound.Play();
    }
    public void Exit()
    {
        Application.Quit();
        ClickSound.Play();
    }
}
