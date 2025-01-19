using System.Collections;
using DG.Tweening;
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

    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    private uint _score;

    
    public new void Awake()
    {
        base.Awake();
        _initialScale = GameplaySettings.PlayerBubbleInitialScale;
        _playerRigidbody2D = GetComponent<Rigidbody2D>();

        _playerInput = new InputControl();
        _pauseGameAction = _playerInput.GP.Pause;
    }

    private void Start()
    {
        StartCoroutine(UpdateScore());
        
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
        _expectLifeTime = (_scale - GameplaySettings.BubbleMinScale) / GameplaySettings.BubbleShrinkRate;

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
        _verticalSpeed = Mathf.Clamp(_verticalSpeed, -1.0f, GameplaySettings.PlayerVerticalMaxSpeed);

        if (_inputMovement.x == 0 && _verticalSpeed == 0)
        {
            return;
        }

        Vector2 inputMovement = new Vector2(_inputMovement.x, _verticalSpeed).normalized;
        float verticalIndex = Mathf.Clamp01(_scale - GameplaySettings.BubbleScaleThreshold) + 1;
        
        _playerRigidbody2D.MovePosition(
            _playerRigidbody2D.position
            + Time.deltaTime * new Vector2(verticalIndex * GameplaySettings.PlayerHorizontalSpeed * inputMovement.x, _verticalSpeed)
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

        _uiLogic.UpdatePlayerInfo($"score: +{_scale * _scale:F1}\nlife: {_expectLifeTime:F1}");
    }

    protected override IEnumerator AfterBurstImpl()
    {
        yield return YieldHelper.WaitForSeconds(1.0f);
        GameplayManager.GetInstance().IsGameOver = true;
        _uiLogic.GameOver();
        yield return null;
    }
    
    private void PauseGame(InputAction.CallbackContext context)
    {
        _uiLogic.PauseGame();
        _uiLogic.SwitchPanel(true);
    }

    private IEnumerator UpdateScore()
    {
        while (!GameplayManager.GetInstance().IsGameOver)
        {
            yield return YieldHelper.WaitForSeconds(1.0f, true);
            _score += (uint)Mathf.CeilToInt(_scale * _scale);
            _uiLogic.UpdateScore(_score);
        }

        yield return null;
    }
}