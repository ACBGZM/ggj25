using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;

    private InputControl _playerInput;
    
    [SerializeField
#if UNITY_EDITOR
     , ReadOnly
#endif
    ]
    private Vector2 _inputMovement;

    [SerializeField]
    private float _speed;


    public void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();

        _playerInput = new InputControl();
    }

    public void OnEnable()
    {
        _playerInput.Enable();
    }
    
    public void OnDisable()
    {
        _playerInput.Disable();
    }

    public void Update()
    {
        ProcessInput();
    }
    
    public void FixedUpdate()
    {
        PlayerMovement();
    }
    
    private void ProcessInput()
    {
        _inputMovement = _playerInput.GP.Move.ReadValue<Vector2>();
    }
    
    
    private void PlayerMovement()
    {
        if (_inputMovement.x != 0 || _inputMovement.y != 0)
        {
            float2 inputMovement = math.normalize(
                new float2(_inputMovement.x, _inputMovement.y)
            );
            float speed = _speed;
            _playerRigidbody2D.MovePosition(
                _playerRigidbody2D.position
                + new Vector2(inputMovement.x, inputMovement.y) * speed * Time.deltaTime
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
    }

}
