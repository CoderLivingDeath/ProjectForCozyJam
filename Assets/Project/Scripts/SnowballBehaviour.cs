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
