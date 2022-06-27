//using Assets.Scripts;
using Assets.Scripts;
using Assets.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.InputSystem;
using Assets.Scripts.Components;
using UnityEditor;
using Assets.Scripts.Components.Model;
using UnityEngine.SceneManagement;
using Assets.Scripts.Components.UI.Hud;
using Assets.Scripts.Components.Model.Definitions;
using Assets.Scripts.Components.Model.Definitions.Repositories;
using Assets.Scripts.Components.ColliderBased;

public class Hero : MonoBehaviour
{
    #region
    [Header("HeroSettings")]
    [Range(1,8)]
    [SerializeField] private float _jumpSpeeed;
    
    [Range(0, 8)]
    [SerializeField] private float _speed;
    [SerializeField] private float _crouchSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float rotationPower = 10;

    [SerializeField] private float _turnSmoothTime = 0.1f;


    [Header("Controls")]
    [SerializeField] private GroundChecker _layerCheck;
    [SerializeField] private CheckSphereComponent _interactionCheck;

    [SerializeField] private Transform _camera;
    [SerializeField] private GameObject _followTransform;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Transform _fireTransform;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _aimCamera;
    public GameObject aimReticle;

    private ThirdPersonController _actions;


    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    private EquipmentSystem _equipment;
    private HealthComponent _health;
    private GameSession _session;



    private Animator _animator;
    private Rigidbody _rigidbody;

    private Vector2 Input;
    private Vector2 MouseInput;

    private bool _isGrounded;
    private bool _isJumpPressing;
    private bool _isMovementPressed;
    private bool _isCrouching;

    private bool _isInCombat;



    private bool _isDashing;
    private bool _isAttacking;
    private bool _isArching;

    public string SelectedId => _session.QuickInventoryModel.SelectedItem.Id;


    private float turnSmoothVelocity;
    #endregion
    #region
    public bool IsArching { get { return _isArching; } set { _isArching = value;} }
    public float JumpSpeeed => _jumpSpeeed;
    public float Speed => _speed;

    public float CrouchSpeed => _crouchSpeed;
   

    public float TurnSmoothTime => _turnSmoothTime;
    public float AccelerationSpeed => _accelerationSpeed;

    public float MaxSpeed => _maxSpeed;
    public float TurnSmoothVelocity { get { return turnSmoothVelocity; } set { turnSmoothVelocity = value; } }

    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool IsJumpPressing { get { return _isJumpPressing; } set { _isJumpPressing = value; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } set { _isMovementPressed = value; } }


    public bool IsCrouching { get { return _isCrouching; } set { _isCrouching = value; } }
    
    public bool IsDashing => _isDashing;

    public bool IsInCombat { get { return _isInCombat; } set { _isInCombat = value; } }
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public GameObject ArrowPrefab { get { return _arrowPrefab; } set { _arrowPrefab = value; } }
    public GameObject Arrow { get { return _arrow; } set { _arrow = value; } }

    public GameObject FollowTransform => _followTransform;
    public Transform FireTransform => _fireTransform;
    public GameObject MainCamera { get { return _mainCamera; } set { _mainCamera = value; } }
    public GameObject AimCamera { get { return _aimCamera; } set { _aimCamera = value; } }
    public Animator Animator => _animator;
    public Rigidbody Rigidbody => _rigidbody;

    public GameSession Session => _session;

    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _health = GetComponent<HealthComponent>();
        _animator = GetComponent<Animator>();
        _actions = new ThirdPersonController();
        _session = FindObjectOfType<GameSession>();

        _health.SetHealth(_session.Data.Hp.Value);

        _states = new PlayerStateFactory(this);
        _currentState = _states.Walk();
        _currentState.EnterState();
    }
    #region
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isJumpPressing = true;
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isMovementPressed = true;
            Input = context.ReadValue<Vector2>();
        }
        if (context.canceled)
        {
            _isMovementPressed = false;
            Input = context.ReadValue<Vector2>();
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        MouseInput = context.ReadValue<Vector2>();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        _isCrouching = context.ReadValueAsButton();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        _isDashing = context.ReadValueAsButton();
    }

    public void OnCombat(InputAction.CallbackContext context)
    {
        _isInCombat = context.ReadValueAsButton();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isAttacking = true;
        }
        if (context.canceled)
        {
            _isAttacking = false;
        }
    }

    public void OnArching(InputAction.CallbackContext context)
    {
        _isArching = context.ReadValueAsButton();
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var hud = HudController.FindObjectOfType<HudController>();
            hud.OnGameWindow();
        }
    }

    public void OnNextItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _session.QuickInventoryModel.SetNextItem();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactionCheck.Check();
        }
    }

    #endregion



    private void FixedUpdate()
    {
        _isGrounded = _layerCheck.Check();

        _currentState.UpdateState();
       
    }

    public Vector3 SetDirection()
    {
        Vector3 position = (transform.forward * Input.y * _speed) + (transform.right * Input.x * _speed);
        return position;
    }


    public void SetLookDirection()
    {
        _followTransform.transform.rotation *= Quaternion.AngleAxis(MouseInput.x * rotationPower, Vector3.up);
        _followTransform.transform.rotation *= Quaternion.AngleAxis(MouseInput.y * rotationPower, Vector3.right);
      

        if (Input.x == 0 && Input.y == 0)
        {
            SetRotation();
            return;
        }
        SetRotation();
    }


    private void SetRotation()
    {
        var angles = VerticalRotation();
        transform.rotation = Quaternion.Euler(0, _followTransform.transform.rotation.eulerAngles.y, 0);
        _followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    private Vector3 VerticalRotation()
    {
        var angles = _followTransform.transform.localEulerAngles;
        angles.z = 0;
        var angle = _followTransform.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        _followTransform.transform.localEulerAngles = angles;
        return angles;
    }
    
    public void OnHealthChanged(int currentHealth)
    {
        _session.Data.Hp.Value = currentHealth;
    }

    public bool IsSelectedDef(ItemTags tag)
    {
        return _session.QuickInventoryModel.SelectedDef.HasTag(tag);
    }

    private void UsePotion()
    {
        var potion = DefsFacade.Instance.Potion.Get(SelectedId);
        switch (potion.Effect)
        {
            case Effect.Heal:
                _session.Data.Hp.Value += (int)potion.Value;
                break;
            case Effect.SpeedUp:
                break;
        }
        _session.Data.Inventory.Remove(potion.Id, 1);
    }

    public void AddInInventory(string id, int value)
    {
        _session.Data.Inventory.Add(id, value);
    }

}






