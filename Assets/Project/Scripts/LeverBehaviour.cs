using UnityEngine;
using UnityEngine.Events;

public class LeverBehaviour : MonoBehaviour, IInteractable
{
    public bool IsHighlithing => _isHightLithing;

    public SpriteRenderer _spriteRenderer;

    public Material StandartMaterial;
    public Material OutlineMaterial;

    private bool IsActive = false;
    private bool CanSwitch = true;

    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    private bool _isHightLithing;

    public UnityEvent<bool> OnSwitch;

    public void Switch()
    {
        if (CanSwitch)
        {
            IsActive = !IsActive;
            _animator.SetBool("IsActive", IsActive);
            OnSwitch.Invoke(!IsActive);
            CanSwitch = false;

            _audioSource.Play();
        }
    }

    public void OnInteract(GameObject sender)
    {
        Switch();
    }

    public void StartHighlight(GameObject sender)
    {
        _spriteRenderer.material = OutlineMaterial;
        _isHightLithing = true;
    }

    public void StopHighLight(GameObject sender)
    {
        _spriteRenderer.material = StandartMaterial;
        _isHightLithing = false;
    }
}
