using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuView : MonoBehaviour
{
    private const int START_SCENE_ID = 1;

    public GameObject MainButtonsView;
    public GameObject SettingsView;

    public Button StartGameButton;

    public Button ShowSettingButton;
    public Button HideSettingButton;

    [Inject] private ISceneLoader sceneLoader;

    private void Start()
    {
        StartGameButton.onClick.AddListener(() => StartGameplay());

        ShowSettingButton.onClick.AddListener(() => ShowSettings());
        HideSettingButton.onClick.AddListener(() => HideSettings());
    }

    public void StartGameplay()
    {
        sceneLoader.LoadScene(START_SCENE_ID);
    }
    public void ShowSettings()
    {
        MainButtonsView.SetActive(false);
        SettingsView.SetActive(true);
    }

    public void HideSettings()
    {
        MainButtonsView.SetActive(true);
        SettingsView.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
