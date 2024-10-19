using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour, PlayerInput.IGameplayActions
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
    }

    private void Start()
    {
        playerInput.Gameplay.Enable();
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
}
