using Assets.Project.Scripts.Infrastructure.EventBus;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour, IPlayerMoveEventHandler
{

    private Rigidbody2D _rigidBody;

    [SerializeField] private float _speed = 10f;

    [SerializeField] private LayerMask _interactableLayer;

    private Vector2 _moveVector;

    private IEventBus _eventBus;

    [Inject]
    private void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Interact()
    {
        float searchRadius = 5f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, _interactableLayer);

        if(hitColliders.Length == 0) return;

        hitColliders[0].GetComponent<IInteractable>().OnInteract();
    }

    public void Handle(Vector2 direction)
    {
        _moveVector = direction;
    }

    #region Unity Methods

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 offet = _moveVector * Time.deltaTime * _speed;
        _rigidBody.MovePosition(_rigidBody.position + offet);

        Interact();
    }

    private void OnEnable()
    {
        _eventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        _eventBus.Unsubscribe(this);
    }

    #endregion
}
