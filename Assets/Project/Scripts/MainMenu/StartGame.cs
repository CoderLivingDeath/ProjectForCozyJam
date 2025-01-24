using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
