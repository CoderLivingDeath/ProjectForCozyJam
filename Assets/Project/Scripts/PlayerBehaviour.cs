using Assets.Project.Scripts.Infrastructure.EventBus;
using System;
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

    [SerializeField] private PlayerModelsConfig _playerModelsConfig;

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
    private void OnSwitchToLarge()
    {
        _animator.runtimeAnimatorController = _playerModelsConfig.Large;
    }

    private void OnSwitchToSmall()
    {
        _animator.runtimeAnimatorController = _playerModelsConfig.Small;
    }

    private void OnSwitchToNormal()
    {
        _animator.runtimeAnimatorController = _playerModelsConfig.Normal;
    }

    private void SwitchPlayerModel(PlayerModelType type)
    {
        switch (type)
        {
            case PlayerModelType.Normal:
                OnSwitchToNormal();
                break;
            case PlayerModelType.Small:
                OnSwitchToSmall();
                break;
            case PlayerModelType.Large:
                OnSwitchToLarge();
                break;
        }
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

public enum PlayerModelType
{
    Normal, Small, Large
}
