using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject MainArea;
    public GameObject OptionsArea;
    public void ShowOptions()
    {
        MainArea.SetActive(false);
        OptionsArea.SetActive(true);
    }

    public void HideOptions()
    {
        MainArea.SetActive(true);
        OptionsArea.SetActive(false);
    }    
}
