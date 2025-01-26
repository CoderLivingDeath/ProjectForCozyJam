using System.Collections;
using UnityEngine;

public class FireContoller : MonoBehaviour
{
    public float Timer = 0.5f;

    public GameObject Fire;

    public void Start()
    {
        StartCoroutine(nameof(StartControl));
    }

    private IEnumerator StartControl()
    {
        while (true)
        {
            yield return new WaitForSeconds(Timer);
            Fire.SetActive(!Fire.activeSelf);
        }
    }
}
