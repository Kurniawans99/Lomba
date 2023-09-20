using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    [Serialize] public bool isControlled = false;
    private GameManager gameManager;
    private PlayerManager playerManager;
    [SerializeField] private GameInput1 gameInput;
    [SerializeField] private GameInput2 gameInput2;

    [SerializeField] float defaultSpeed;
    private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float radius;
    private SphereCollider sphereCollider;
    Vector3 moveDir;
    private bool isRunning;
    public bool handrise = false;
    private Collider previousCollider = null;

    private void Start()
    {

        gameInput.OnRun += GameInput_OnRun;
        gameInput.OnTouch += GameInput_OnTouch;
      gameInput.OnSwitch += GameInput_OnSwitch;

        gameInput2.OnRun += GameInput_OnRun2;
        gameInput2.OnTouch += GameInput_OnTouch2;
       gameInput2.OnSwitch += GameInput_OnSwitch2;





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

    private void GameInput_OnSwitch(object sender,EventArgs e)
    {
        if(isControlled && playerManager.team == 0)
        {
            if (gameManager.currentIdA >= 4) 
            {
                gameManager.currentIdA = 0;

            }
            if (gameManager.currentIdA < 4)
            {

                gameManager.currentIdA += 1;

            }

            

            foreach (PlayerManager player in gameManager.players)
            {
                if (playerManager.team == player.team && player.idPlayer == gameManager.currentIdA)
                {
                    isControlled = false;
                    player.SwitchControl();
                    playerManager.botDummy.currentState = BotState.Idle;
                    break;
                }
            }


        }





    }
    private void GameInput_OnSwitch2(object sender, EventArgs e)
    {
        if (isControlled && playerManager.team == 1)
        {
            if (gameManager.currentIdB >= 4)
            {
                gameManager.currentIdB = 0;

            }
            if (gameManager.currentIdB < 4)
            {

                gameManager.currentIdB += 1;

            }



            foreach (PlayerManager player in gameManager.players)
            {
                if (playerManager.team == player.team && player.idPlayer == gameManager.currentIdB)
                {
                    isControlled = false;
                    player.SwitchControl();
                    playerManager.botDummy.currentState = BotState.Idle;

                    break;
                }
            }


        }

    }

    private void GameInput_OnRun(object sender, GameInput1.OnRunEventArgs e)
    {
        isRunning = e.isRunning;
        if (isRunning && isControlled)
        {
            speed = defaultSpeed * 2;
        }
        else
        {
            speed = defaultSpeed;
        }


    }
    private void GameInput_OnTouch(object sender, GameInput1.OnTouchEventArgs e)
    {

        if (e.isTouching && isControlled && playerManager.team == 0)
        {


            handrise = true;

        }
        else
        {
            handrise = false;
        }

    }

    private void GameInput_OnRun2(object sender, GameInput2.OnRunEventArgs e)
    {
        isRunning = e.isRunning;
        if (isRunning && isControlled)
        {
            speed = defaultSpeed * 2;
        }
        else
        {
            speed = defaultSpeed;
        }


    }
    private void GameInput_OnTouch2(object sender, GameInput2.OnTouchEventArgs e)
    {

        if (e.isTouching && isControlled && playerManager.team == 1)
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
        if (isControlled && !playerManager.onCatch)
        {
            HandleMovement();
            HandleTouch();

        }
        else if(!isControlled && !playerManager.onCatch)
        {
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
        if(playerManager.team == 1)
        {
            Vector2 inputValue = gameInput2.GetMovementVector();
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
        }else if(playerManager.team == 0) {

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

    public bool CheckOnCatcth()
    {
        return playerManager.onCatch;
    }
    public int checkTeamPlayer(){

        return playerManager.team;
    }

}