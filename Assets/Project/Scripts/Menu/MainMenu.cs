using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MainMenu : MonoBehaviour
{
    private const int START_SCENE_ID = 1;

    public GameObject MainArea;
    public GameObject OptionsArea;
    public AudioSource ClickSound;

    [Inject] ISceneLoader sceneLoader;

    public void Play()
    {
        sceneLoader.LoadScene(START_SCENE_ID);
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
