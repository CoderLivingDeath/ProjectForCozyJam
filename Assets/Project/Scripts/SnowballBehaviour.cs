using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class SnowballBehaviour : MonoBehaviour, IInteractable
{
    public Material StandartMaterial;
    public Material OutlineMaterial;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private bool _isHightLithing;
    public bool IsHifglithing => _isHightLithing;

    public void OnInteract(GameObject sender)
    {
        OnPlayerTakeSnowBall(sender.GetComponent<PlayerBehaviour>());
    }

    public void OnPlayerTakeSnowBall(PlayerBehaviour player)
    {
        if(CanRisePlayerModelType(player))
        {
            player.RisePlayerModelState();
            Destroy(this.gameObject);
        }
    }

    private bool CanRisePlayerModelType(PlayerBehaviour player)
    {
        return player.PlayerModelType != PlayerModelType.Large;
    }

    public void StartHighlight(GameObject sender)
    {
        if(_spriteRenderer == null) return;
        _isHightLithing = true;
    }

    public void StopHighLight(GameObject sender)
    {
        if (_spriteRenderer == null) return;
        _isHightLithing = false;
    }

    private void FixedUpdate()
    {
        if(_isHightLithing)
        {
            _spriteRenderer.material = OutlineMaterial;
        }
        else
        {
            _spriteRenderer.material = StandartMaterial;
        }
    }
}
