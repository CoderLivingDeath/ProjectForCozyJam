using UnityEngine;

public class LeverBehaviour : MonoBehaviour, IInteractable
{
    private bool IsActive = false;
    private bool CanSwitch = true;

    [SerializeField] private Animator _animator;

    public void Switch()
    {
        Debug.Log(IsActive);
        if (CanSwitch) _animator.SetBool("IsActive", IsActive = !IsActive);
    }

    public void OnInteract()
    {
        Switch();
    }
}
