using Assets.Project.Scripts.Infrastructure.EventBus;
using Assets.Project.Scripts.Infrastructure.EventBus.EventHandlers;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour, IPlayerMoveEventHandler, IPlayerInteractEventHanlder
{

    public PlayerModelType PlayerModelType => _currentModelType;

    private Rigidbody2D _rigidBody;

    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _smooth = 0.5f;

    [SerializeField] private LayerMask _interactableLayer;

    [SerializeField] private Animator _animator;

    [SerializeField] private PlayerModelsConfig _playerModelsConfig;

    private PlayerModelType _currentModelType;
    private Vector2 _moveVector;
    private Vector2 _smoothMoveVector;
    private bool _isRight;

    public GameObject PousePanel;

    private IInteractable _selectedInteractable;

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

        hitColliders[0].GetComponent<IInteractable>().OnInteract(this.gameObject);
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

    public void RisePlayerModelState()
    {
        switch (_currentModelType)
        {
            case PlayerModelType.Normal: SwitchPlayerModel(PlayerModelType.Large); break;
            case PlayerModelType.Small: SwitchPlayerModel(PlayerModelType.Normal); break;
            case PlayerModelType.Large: break;
        }
    }

    public void DowngradePlayerModelState()
    {
        switch (_currentModelType)
        {
            case PlayerModelType.Normal: SwitchPlayerModel(PlayerModelType.Small); break;
            case PlayerModelType.Small: break;
            case PlayerModelType.Large: SwitchPlayerModel(PlayerModelType.Normal); break;
        }
    }

    public void HightLightInteractableObjectNearPlayer()
    {
        Vector2 mousePositopn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseSearchRadiud = 5f;
        Collider2D[] hitCollidersNearMouse = Physics2D.OverlapCircleAll(mousePositopn, mouseSearchRadiud, _interactableLayer);

        float searchRadius = 5f;
        Collider2D[] hitCollidersNearPlayer = Physics2D.OverlapCircleAll(transform.position, searchRadius, _interactableLayer);

        var interset = hitCollidersNearMouse.Intersect(hitCollidersNearPlayer);

        if(interset.Count() == 0)
        {
            _selectedInteractable?.StopHigthLith(this.gameObject);
            _selectedInteractable = null;

            return;
        }

        var filtered = interset.OrderBy(x => Vector2.Distance((Vector2)x.transform.position, mousePositopn));
        var selectedObject = filtered.First().gameObject;
        var interactable = selectedObject.GetComponent<IInteractable>();


        if(interactable != _selectedInteractable)
        {
            _selectedInteractable?.StopHigthLith(this.gameObject);
            _selectedInteractable = interactable;
        }

        if (!interactable.IsHifglithing)
        {
            interactable?.StartHigthLith(this.gameObject);
        }
    }

    public void Handle()
    {
        throw new NotImplementedException();
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

        HightLightInteractableObjectNearPlayer();
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
