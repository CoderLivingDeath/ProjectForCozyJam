using UnityEngine;

public class LeverBehaviour : MonoBehaviour, IInteractable
{
    public bool IsHifglithing => _isHightLithing;

    public SpriteRenderer _spriteRenderer;

    public Material StandartMaterial;
    public Material OutlineMaterial;

    private bool IsActive = false;
    private bool CanSwitch = true;

    [SerializeField] private Animator _animator;
    private bool _isHightLithing;

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
        _spriteRenderer.material = OutlineMaterial;
        _isHightLithing = true;
    }

    public void StopHigthLith(GameObject sende)
    {
        _spriteRenderer.material = StandartMaterial;
        _isHightLithing = false;
    }
}
