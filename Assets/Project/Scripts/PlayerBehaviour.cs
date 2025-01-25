using Assets.Project.Scripts.Infrastructure.EventBus;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour, IPlayerMoveEventHandler
{

    private Rigidbody2D _rigidBody;

    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _smooth = 0.5f;

    [SerializeField] private LayerMask _interactableLayer;

    [SerializeField] private Animator _animator;

    private Vector2 _moveVector;
    private Vector2 _smoothMoveVector;
    private bool _isRight;

    public GameObject PousePanel;

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

        if (hitColliders.Length == 0) return;

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
        if (PousePanel.activeSelf)
            _moveVector = Vector2.zero;

        _animator.SetBool("IsWalk", _moveVector != Vector2.zero);

        if (_moveVector == Vector2.zero)
            OnStay();
        else
            OnMove();
    }

    private void OnMove()
    {
        if (_moveVector.x > 0)
            transform.localScale = new Vector3(1, 1, 0);
        else
            transform.localScale = new Vector3(-1, 1, 0);

        _smoothMoveVector = Vector2.Lerp(_smoothMoveVector, _moveVector.normalized, _smooth).normalized;

        Vector2 offet = _smoothMoveVector * Time.deltaTime * _speed;
        _rigidBody.MovePosition(_rigidBody.position + offet);

        _smoothMoveVector = Vector2.zero;
    }

    private void OnStay()
    {
        _smoothMoveVector = Vector2.zero;
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
