using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler<OnRunEventArgs> OnRun;
    public event EventHandler OnDash;
    public class OnRunEventArgs : EventArgs
    {
        public bool isRunning;
    }
    private PlayerInputActions playerInputActions;
    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Run.performed += PlayerRun_Performed;
        playerInputActions.Player.Run.canceled += PlayerRun_Canceled;
        playerInputActions.Player.Dash.started += PlayerDash_Started;
    }

    private void PlayerDash_Started(InputAction.CallbackContext context)
    {
        OnDash?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerRun_Performed(InputAction.CallbackContext context)
    {
        OnRun?.Invoke(this, new OnRunEventArgs
        {
            isRunning = context.performed
        });
    }
    private void PlayerRun_Canceled(InputAction.CallbackContext context)
    {
        OnRun?.Invoke(this, new OnRunEventArgs
        {
            isRunning = context.performed
        });
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputValue = Vector2.zero;
        inputValue = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputValue.normalized;
    }
}
