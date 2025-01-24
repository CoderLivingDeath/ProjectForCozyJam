using UnityEngine;

public class LeverBehaviour : MonoBehaviour, IInteractable
{
    private bool IsActive = false;
    private bool CanSwitch = true;

    public void Switch()
    {
        Debug.Log(IsActive);
        if (CanSwitch) IsActive = !IsActive;
    }

    public void OnInteract()
    {
        Switch();
    }
}
