using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuInput : MonoBehaviour
{
    private InputControl _playerInput;
    private InputAction _startGameAction;
    
    public Button myButton;
    
    public void Awake()
    {
        _playerInput = new InputControl();
        _startGameAction = _playerInput.GP.A;
    }
    
    public void OnEnable()
    {
        _playerInput.Enable();
        _startGameAction.performed += StartGame;
    }
    
    public void OnDisable()
    {
        _startGameAction.performed -= StartGame;
        _playerInput.Disable();
    }
    
    public void StartGame(InputAction.CallbackContext context)
    {
        myButton.onClick.Invoke();
    }
}
