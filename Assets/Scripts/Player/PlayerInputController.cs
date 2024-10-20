using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour, PlayerInput.IGameplayActions, PlayerInput.IUIActions
{
    private PlayerInput playerInput;
    
    public event Action<float> OnMoveInput;
    public event Action OnShootInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Gameplay.SetCallbacks(this);
        playerInput.UI.SetCallbacks(this);

        EventManager.OnGameEnded += EnableUIInput;
        EventManager.OnGameRestarted += EnableGameplayInput;
    }

    private void OnDisable()
    {
        EventManager.OnGameEnded -= EnableUIInput;
        EventManager.OnGameRestarted -= EnableGameplayInput;
    }

    private void Start()
    {
        playerInput.Gameplay.Enable();
    }

    private void EnableGameplayInput()
    {
        playerInput.Gameplay.Enable();
        playerInput.UI.Disable();
    }

    private void EnableUIInput()
    {
        playerInput.UI.Enable();
        playerInput.Gameplay.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(context.ReadValue<float>());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            OnShootInput?.Invoke();
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
       if (context.phase == InputActionPhase.Performed)
            SceneManager.LoadScene(0);
    }
}
