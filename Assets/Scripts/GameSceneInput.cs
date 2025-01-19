using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameSceneInput : MonoBehaviour
{
    private InputControl _playerInput;
    private InputAction _resumeGameAction;
    private InputAction _backToMainMenuAction;
    
    public Button resumeButton;
    public Button mainMenuButton1;
    public Button mainMenuButton2;
    
    public void Awake()
    {
        _playerInput = new InputControl();
        _resumeGameAction = _playerInput.GP.A;
        _backToMainMenuAction = _playerInput.GP.B;
    }
    
    public void OnEnable()
    {
        _playerInput.Enable();
        _resumeGameAction.performed += ResumeGame;
        _backToMainMenuAction.performed += BackToMainMenu;
    }
    
    public void OnDisable()
    {
        _resumeGameAction.performed -= ResumeGame;
        _backToMainMenuAction.performed -= BackToMainMenu;
        _playerInput.Disable();
    }
    
    public void ResumeGame(InputAction.CallbackContext context)
    {
        if (resumeButton.isActiveAndEnabled)
        {
            resumeButton.onClick.Invoke();
        }
    }
    
    public void BackToMainMenu(InputAction.CallbackContext context)
    {
        if (mainMenuButton1.isActiveAndEnabled)
        {
            mainMenuButton1.onClick.Invoke();
        }
        else if (mainMenuButton2.isActiveAndEnabled)
        {
            mainMenuButton2.onClick.Invoke();
        }
    }
}