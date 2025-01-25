using UnityEngine;

public class LeverBehaviour : MonoBehaviour, IInteractable
{
    private bool IsActive = false;
    private bool CanSwitch = true;

    [SerializeField] private Animator _animator;
    private bool _isHightLithing;

    public bool IsHifglithing => throw new System.NotImplementedException();

    public void Switch()
    {
        Debug.Log(IsActive);
        if (CanSwitch) _animator.SetBool("IsActive", IsActive = !IsActive);
    }

    public void OnInteract(GameObject sender)
    {
        Switch();
    }

    public void StartHigthLith(GameObject sender)
    {
        _isHightLithing = true;
    }

    public void StopHigthLith(GameObject sende)
    {
        _isHightLithing = false;
    }
}
