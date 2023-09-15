using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Serialize] public bool isControlled = false;
    private CastleManage castleManage;
    private GameManager gameManager;
    private PlayerManager playerManager;
    public event EventHandler OnPower;
    [SerializeField] private GameInput gameInput;
    [SerializeField] float defaultSpeed;
    private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float radius;
    private SphereCollider sphereCollider;
    Vector3 moveDir;
    private bool isRunning;
    private bool handrise = false;
    private Collider previousCollider = null;




    /* private float cooldownDash = 2f;
     private float speedDash = 2.5f;
     private float dashTime = 0.2f;
     private bool isDashing = false;
     private bool canDash = true;*/
    private void Start()
    {
        gameInput.OnRun += GameInput_OnRun;
        gameInput.OnTouch += GameInput_OnTouch;
        playerManager = GetComponent<PlayerManager>();
        speed = defaultSpeed;
        sphereCollider = GetComponentInChildren<SphereCollider>();
        gameManager = FindObjectOfType<GameManager>();






    }




    //make sure use controller each char so theres no double catach on same time
    void OnCollisionEnter(Collision colInfo)
    {
        previousCollider = colInfo.collider; // Store the current collider

        // Rest of your code...
    }

    void OnCollisionExit(Collision colInfo)
    {
        if (colInfo.collider == previousCollider)
        {
            previousCollider = null; // Reset the previous collider when it exits
        }
    }

    /*    private IEnumerator Dash()
        {
            float originalSpeed = speed;
            speed *= speedDash;
            isDashing = true;
            canDash = false;
            yield return new WaitForSeconds(dashTime);
            speed = originalSpeed;
            isDashing = false;
            yield return new WaitForSeconds(cooldownDash);
            canDash = true;


        }*/

    private void GameInput_OnRun(object sender, GameInput.OnRunEventArgs e)
    {
        isRunning = e.isRunning;
        if (isRunning)
        {
            speed = defaultSpeed * 2;
        }
        else
        {
            speed = defaultSpeed;
        }


    }
    private void GameInput_OnTouch(object sender, GameInput.OnTouchEventArgs e)
    {
        
        if (e.isTouching)
        {
           
            
            handrise = true;
            
        }
        else
        {
            handrise = false;
        }

    }
    private void Update()
    {
        if (isControlled)
        {
            HandleMovement();
            HandleTouch();

        }


    }

    private void HandleTouch()
    {
        if (handrise && previousCollider != null)
        {
            PlayerManager otherPlayerManager = previousCollider.GetComponentInParent<PlayerManager>();

            if (otherPlayerManager != null)
            {
                if (otherPlayerManager.team != playerManager.team && !otherPlayerManager.onSaveZone)
                {
                    otherPlayerManager.OnTouchingOtherPlayer(playerManager);
                    // Logic for touching enemy player
                }

                if (otherPlayerManager.team == playerManager.team && otherPlayerManager.onCatch)
                {
                    otherPlayerManager.ReleaseFromArrest();
                        // Logic for releasing player
                }
            }
            else
            {
                CastleManage whoCastle = previousCollider.gameObject.GetComponent<CastleManage>();

                if (whoCastle != null)
                {
                    if (whoCastle.CastleTeam == playerManager.team)
                    {
                        playerManager.TouchingCastleAlly();
                        // Logic for resetting time
                    }
                    else if(whoCastle.CastleTeam != playerManager.team)
                    {
                        
                        Debug.Log("Win");
                        gameManager.WinRound(playerManager.team);
                        
                        // Logic for winning
                    }
                }
            }
            
        }
    }
    private void HandleMovement()
    {
        Vector2 inputValue = gameInput.GetMovementVector();
        moveDir = new Vector3(inputValue.x, 0, inputValue.y);
        bool canMove = !CheckCollision(moveDir);
        if (!canMove)
        {
            moveDir = CheckAxis(moveDir);
            canMove = !CheckCollision(moveDir);
        }
        if (canMove)
        {

            transform.position += moveDir * speed * Time.deltaTime;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    private Vector3 CheckAxis(Vector3 moveDir)
    {
        bool moveInOneAxis;
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
        moveInOneAxis = !CheckCollision(moveDirX) && moveDirX != Vector3.zero;
        if (moveInOneAxis)
        {
            return moveDirX;
        }
        else
        {
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
            moveInOneAxis = !CheckCollision(moveDirZ) && moveDirZ != Vector3.zero;
            if (moveInOneAxis)
            {
                return moveDirZ;
            }
            return moveDir;
        }

    }
    private bool CheckCollision(Vector3 moveDir)
    {
        /*        float playerHeight = 2f;
                float radiusCast = radius;
                if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, radiusCast, moveDir, speed * Time.deltaTime))
                {
                    return true;
                }
                else return false;
        */
        return false;

    }

    public Vector3 IsMoving()
    {
        return moveDir;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

}