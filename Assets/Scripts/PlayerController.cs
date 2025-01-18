using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Bubble
{
    private Rigidbody2D _playerRigidbody2D;
    private InputControl _playerInput;
    
    [SerializeField] private UILogic _uiLogic;
    private InputAction _pauseGameAction;
    
    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    private Vector2 _inputMovement;

    [SerializeField]
    private float _speed;


    public new void Awake()
    {
        base.Awake();
        _initialScale = GameplaySettings.PlayerBubbleInitialScale;
        _playerRigidbody2D = GetComponent<Rigidbody2D>();

        _playerInput = new InputControl();
        _pauseGameAction = _playerInput.GP.Pause;
    }

    public void OnEnable()
    {
        _playerInput.Enable();
        _pauseGameAction.performed += PauseGame;
    }
    
    public void OnDisable()
    {
        _pauseGameAction.performed -= PauseGame;
        _playerInput.Disable();
    }

    public void Update()
    {
        ProcessInput();
    }
    
    public new void FixedUpdate()
    {
        _expectLifeTime = (_scale - _minScale) / _shrinkRate;

        Shrink();
        PlayerMovement();
    }
    
    private void ProcessInput()
    {
        _inputMovement = _playerInput.GP.Move.ReadValue<Vector2>();
    }
    
    private void PlayerMovement()
    {
        CalculateVerticalSpeed();
        _verticalSpeed *= 0.5f;

        if (_inputMovement.x == 0 && _verticalSpeed == 0)
        {
            return;
        }
        
        float2 inputMovement = math.normalize(
            new float2(_inputMovement.x, _verticalSpeed)
        );
        float speed = _speed;
        _playerRigidbody2D.MovePosition(
            _playerRigidbody2D.position
            + speed * Time.deltaTime * new Vector2(inputMovement.x, _verticalSpeed)
        );

        if (_inputMovement.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }
        else if (_inputMovement.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }
    }
    
    private void PauseGame(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _uiLogic.PauseGame();
        _uiLogic.SwitchPanel(true);
    }
}
