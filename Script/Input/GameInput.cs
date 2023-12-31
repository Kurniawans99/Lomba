using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput2 : MonoBehaviour
{
    
    public event EventHandler<OnRunEventArgs> OnRun;
    public event EventHandler<OnTouchEventArgs> OnTouch;
    public event EventHandler OnSwitch;
    public class OnRunEventArgs : EventArgs
    {
        public bool isRunning;
    }

    public class OnTouchEventArgs : EventArgs
    {
        public bool isTouching;
    }
    private PlayerInputActions playerInputActions;
    void Awake()
    {
        /*   playerInputActions = GetComponent<PlayerInput>().actions;
         *   if(playerteam == 1)
         *   {
           playerActionMap = playerInputActions.FindActionMap("keyboard1");
        }else if(playerteam == 2){
                playerActionMap = playerInputActions.FindActionMap("keyboard1");
        }
           */

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player2.Enable();
        
        playerInputActions.Player2.Run.performed += PlayerRun_Performed;
        playerInputActions.Player2.Run.canceled += PlayerRun_Canceled;
        playerInputActions.Player2.Touch.performed += PlayerTouch_Performed;
        playerInputActions.Player2.Touch.canceled += PlayerTouch_Canceled;
        playerInputActions.Player2.Switch.started += PlayerSwitch_Started;
    }
    private void PlayerSwitch_Started(InputAction.CallbackContext context)
    {
        OnSwitch?.Invoke(this, EventArgs.Empty);
    }
    private void PlayerTouch_Performed(InputAction.CallbackContext context)
    {
        OnTouch?.Invoke(this, new OnTouchEventArgs
        {
            isTouching = context.performed
        });
    }
    private void PlayerTouch_Canceled(InputAction.CallbackContext context)
    {
        OnTouch?.Invoke(this, new OnTouchEventArgs
        {
            isTouching = context.performed
        });
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
        inputValue = playerInputActions.Player2.Move.ReadValue<Vector2>();
        return inputValue.normalized;
    }
}
