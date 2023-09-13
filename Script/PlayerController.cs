using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event EventHandler OnPower;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float radius;
    private float cooldownDash = 2f;
    private float speedDash = 2.5f;
    private float dashTime = 0.2f;
    private bool isDashing = false;
    private bool canDash = true;
    private void Start()
    {
        gameInput.OnRun += GameInput_OnRun;
        gameInput.OnDash += GameInput_OnDash;
    }

    private void GameInput_OnDash(object sender, EventArgs e)
    {
        if(canDash)
        {

        StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
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
         

    }

    private void GameInput_OnRun(object sender, GameInput.OnRunEventArgs e)
    {
        if (!isDashing)
        {
          if (e.isRunning)
            {
            speed = 10;
             }
            else
            {
                speed = 5;
            }

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
        float playerHeight = 2f;
        float radiusCast = radius;
        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, radiusCast, moveDir, speed * Time.deltaTime))
        {
            return true;
        }
        else return false;

    }


}
