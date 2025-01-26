using Assets.Project.Scripts;
using Assets.Project.Scripts.Infrastructure.EventBus;
using Assets.Project.Scripts.Infrastructure.EventBus.EventHandlers;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour, IPlayerMoveEventHandler, IPlayerInteractEventHanlder, IPlayerDropMassEventHandler
{
    public PlayerModelType PlayerModelType => _currentModelType;

    public bool GiftSpizhen = false;

    public GameObject SnowballPrefab;

    private Rigidbody2D _rigidBody;

    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _smooth = 0.5f;

    [SerializeField] private LayerMask _interactableLayer;

    [SerializeField] private Animator _animator;

    [SerializeField] private PlayerModelsConfig _playerModelsConfig;

    [SerializeField] private PlayerModelType _currentModelType = PlayerModelType.Normal;

    [SerializeField] private AudioSource _audioWalk;
    [SerializeField] private AudioSource _audioTranformation;

    private Vector2 _moveVector;
    private Vector2 _smoothMoveVector;

    private bool _walkSoundIsPlay = false;

    private IEventBus _eventBus;

    [Inject]
    private void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void PlayInteractAnimation()
    {
        _animator.SetBool("IsInteract", true);
    }

    private void Interact()
    {

        PlayInteractAnimation();

        float searchRadius = 2f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, _interactableLayer);

        if (hitColliders.Length == 0) return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hitColliders.OrderBy(x => Vector2.Distance(x.transform.position, mousePosition)).First().GetComponent<IInteractable>().OnInteract(this.gameObject);
    }

    private void OnSwitchToLarge()
    {
        _currentModelType = PlayerModelType.Large;
        _animator.runtimeAnimatorController = _playerModelsConfig.Large;
        _speed = _playerModelsConfig.LargeSpeed;
    }

    private void OnSwitchToSmall()
    {
        _currentModelType = PlayerModelType.Small;
        _animator.runtimeAnimatorController = _playerModelsConfig.Small;
        _speed = _playerModelsConfig.Smallspeed;
    }

    private void OnSwitchToNormal()
    {
        _currentModelType = PlayerModelType.Normal;
        _animator.runtimeAnimatorController = _playerModelsConfig.Normal;
        _speed = _playerModelsConfig.NoramalSpeed;
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
        _audioTranformation.Play();
    }
    private void HightLightInteractableObjectNearPlayer(float searchRadiusNearPlayer, float serchRadiusNearMouse)
    {
        Vector2 mousePositionInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        OutlineHighlightHalper.SelectObjectNearMouseAndPlayer(transform.position, mousePositionInWorldSpace, searchRadiusNearPlayer, serchRadiusNearMouse, _interactableLayer, this.gameObject);
    }

    private void DropMass()
    {
        if (_currentModelType == PlayerModelType.Small) return;

        Instantiate(SnowballPrefab, transform.position, Quaternion.identity);
        DowngradePlayerModelState();
    }

    private void PlayWalkSound()
    {
        if (!_walkSoundIsPlay)
        {
            _walkSoundIsPlay = true;
            _audioWalk.Play();
        }

    }

    private void StopWalkSound()
    {
        if (_walkSoundIsPlay)
        {
            _walkSoundIsPlay = false;
            _audioWalk.Stop();
        }
    }

    private void OnMove()
    {
        if (_moveVector.x > 0)
            transform.localScale = new Vector3(1, 1, 0);
        else
            transform.localScale = new Vector3(-1, 1, 0);

        _smoothMoveVector = Vector2.Lerp(_smoothMoveVector, _moveVector.normalized, _smooth).normalized;

        Vector2 offet = _smoothMoveVector * Time.fixedDeltaTime * _speed;
        _rigidBody.MovePosition(_rigidBody.position + offet);

        _smoothMoveVector = Vector2.zero;
    }

    private void OnStay()
    {
        _smoothMoveVector = Vector2.zero;
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

    public void InteractHandle()
    {
        Interact();
    }

    public void MoveHandle(Vector2 direction)
    {
        _moveVector = direction;
    }

    public void DropMassHandle()
    {
        DropMass();
    }

    #region Unity Methods

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        SwitchPlayerModel(_playerModelsConfig.StartPlayerModelType);
    }

    private void FixedUpdate()
    {
        _animator.SetBool("IsWalk", _moveVector != Vector2.zero);

        if (_moveVector == Vector2.zero)
        {
            StopWalkSound();
            OnStay();
        }
        else
        {
            PlayWalkSound();
            OnMove();
        }

        HightLightInteractableObjectNearPlayer(3f, 1f);
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
