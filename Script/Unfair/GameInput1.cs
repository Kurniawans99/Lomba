using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput1 : MonoBehaviour
{
    
    public event EventHandler<OnRunEventArgs> OnRun;
    public event EventHandler<OnTouchEventArgs> OnTouch;
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
        playerInputActions.Player1.Enable();
        
        playerInputActions.Player1.Run.performed += PlayerRun_Performed;
        playerInputActions.Player1.Run.canceled += PlayerRun_Canceled;
        playerInputActions.Player1.Touch.performed += PlayerTouch_Performed;
        playerInputActions.Player1.Touch.canceled += PlayerTouch_Canceled;
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
        inputValue = playerInputActions.Player1.Move.ReadValue<Vector2>();
        return inputValue.normalized;
    }
}
