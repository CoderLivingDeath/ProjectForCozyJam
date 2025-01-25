using UnityEngine;
using UnityEngine.SceneManagement;

public class GayMenu : MonoBehaviour
{
    public GameObject PouseScreen;
    public AudioSource ClickSound;
    public void OnPouseClick()
    {
        ClickSound.Play();
        PouseScreen.SetActive(true);
    }
    public void OnPlayClick()
    {
        ClickSound.Play();
        PouseScreen.SetActive(false);
    }
    public void OnExitClick()
    {
        ClickSound.Play();
        SceneManager.LoadScene("MainMenu");
    }
}
