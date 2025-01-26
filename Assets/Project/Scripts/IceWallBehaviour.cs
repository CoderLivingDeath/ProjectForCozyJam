using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IceWallBehaviour : MonoBehaviour, IInteractable
{
    public Material StandartMaterial;
    public Material OutlineMaterial;

    public GameObject DestroyParticalFXPrefab;

    public bool IsHighlithing => _isHighlithing;
    private bool _isHighlithing;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void PlayDestroyFX()
    {
        var fx = Instantiate(DestroyParticalFXPrefab, transform.position, Quaternion.identity);
        fx.GetComponent<DestroyParticalFXBehaviour>().Play();
    }

    public void OnInteract(GameObject sender)
    {
        TryDestroy(sender.GetComponent<PlayerBehaviour>());
    }

    public void TryDestroy(PlayerBehaviour playerBehaviour)
    {
        if (playerBehaviour.PlayerModelType == PlayerModelType.Large)
        {
            PlayDestroyFX();
            Destroy(this.gameObject);
        }
    }

    public void StartHighlight(GameObject sender)
    {
        if (_spriteRenderer == null) return;
        _isHighlithing = true;
    }

    public void StopHighLight(GameObject sender)
    {
        if (_spriteRenderer == null) return;
        _isHighlithing = false;
    }

    private void FixedUpdate()
    {
        if (_isHighlithing)
        {
            _spriteRenderer.material = OutlineMaterial;
        }
        else
        {
            _spriteRenderer.material = StandartMaterial;
        }
    }
}
