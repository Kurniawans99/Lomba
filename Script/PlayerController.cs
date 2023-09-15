using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CastleManage castleManage;
    private PlayerManager playerManager;
    public event EventHandler OnPower;
    [SerializeField] private GameInput gameInput;
    [SerializeField] float defaultSpeed;
    private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float radius;
    private SphereCollider sphereCollider;

    private bool handrise = false;
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

    }



    //make sure use controller each char so theres no double catach on same time
    void OnCollisionEnter(Collision colInfo)
    {
        if (colInfo.collider.tag == "char")
        {
            PlayerManager otherPlayerManager = colInfo.collider.GetComponentInParent<PlayerManager>();
            
            if (handrise && otherPlayerManager.team != playerManager.team && !otherPlayerManager.onSaveZone) {
                Debug.Log(otherPlayerManager.team);

                // playerManager.OnTouchingOtherPlayer(otherPlayerManager);

            }

            if(handrise && otherPlayerManager.team == playerManager.team && otherPlayerManager.onCatch)
            {
                Debug.Log("releasePlayer");

            }
            //&& player pressing E -> check player.team (enemy) then timer then logic to arrest
            //&& player pressing E -> check player.team (ally)then  then logic to release arrest

        }
        if (colInfo.collider.tag == "castle")
        {
            CastleManage whoCastle = colInfo.collider.gameObject.GetComponent<CastleManage>();

            if (handrise && whoCastle.CastleTeam == playerManager.team) {

                Debug.Log("Reset TIme");
            }
            if(handrise && whoCastle.CastleTeam != playerManager.team)
            {
                Debug.Log("Win");
            }
            //&& player pressing E -> check castle.team then logic to set

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
      
            if (e.isRunning)
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
        } else
        {
            handrise =false;
        }

    }
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputValue = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputValue.x, 0, inputValue.y);
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



}
