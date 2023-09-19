using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum BotState
{
    Idle,
    BackToBase,
    ChasingEnemy,
    Hooking,
    Patrolling

}

/*
 * first all bot going to defend then set bot communication to initial the team
2 player attack base but if theres enemy had higher point one should be avoid player, 
and should be idle wait until the player comming to warn distand
player who got in savezone going to attack the base & player who got warn distance comming to defendbase
 */
public class BotDummy : MonoBehaviour
{
    private float timeDeadLine;
    public BotState currentState;
    private NavMeshAgent agent;
    private Animator animator;
    private PlayerController playerController;
    private PlayerManager playerManager;
    public bool onAvoidingPlayer = false;
    private BotCom botCom;
    private PlayerAnimation playerAnimation;
    private GameManager gameManager;
    private PlayerManager otherPlayerWare;

    private float distance;

    public Transform target;
    private bool readyFight;

    private float transitionDelay = 2f; // Adjust this value to set the delay time
    private float transitionTimer = 0f; // Timer to keep track of the delay

    private void Start()
    {
         playerAnimation = GetComponentInChildren<PlayerAnimation>();
        botCom = FindObjectOfType<BotCom>(); 
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
        playerManager = GetComponent<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();


        currentState = BotState.BackToBase;
    }

    private void FixedUpdate()
    {
        if (!playerController.isControlled)
        {
            foreach (PlayerManager splayer in gameManager.players)
            {
                if (splayer.team != playerManager.team && !splayer.onSaveZone && !splayer.onCatch)
                {

                    otherPlayerWare = splayer;
                    checkplayer(splayer);
                }
            }
        }
        
    }
    void Update()
    {
        //check if theres a people with high team near from here
        if (!playerController.isControlled && !playerManager.onCatch)
        {
            switch (currentState)
            {
                case BotState.Idle:
                    // Implement logic for Idle state
                    idle();

                    if (!playerManager.onSaveZone) 
                    {
                        agent.speed = 15f;
                        if (distance < 20)
                        {
                            currentState = BotState.BackToBase;
                            readyFight = false;
                        }
                        else
                        {
                            readyFight = true;
                        }
                        
                    }else if (playerManager.onSaveZone ) {
                        agent.speed = 15f;
                        if (playerManager.team == 0 && botCom.OnBaseBlue > 1)
                            {
                            currentState =BotState.Hooking;
                            }else if(playerManager.team == 1 && botCom.OnBaseRed > 1)
                        {
                            currentState =BotState.Hooking;
                        }
                    
                    }

                    break;

                case BotState.BackToBase:
                    
                    target = botCom.GetTargetPos(2, playerManager);
                    goBase();
                    break;

                case BotState.ChasingEnemy:
                    // Implement logic for ChasingEnemy state
                    ChasingPlayer();
                    break;

                case BotState.Hooking:
                    agent.speed = 15f;
                    walkRandom();
                    /*if (*//* Condition for transitioning to Patrolling *//*)
                    {
                        currentState = BotState.Patrolling;
                    }*/
                    break;

                case BotState.Patrolling:
                    // Implement logic for Patrolling state
                    /*if (*//* Condition for transitioning to Idle *//*)
                    {
                        currentState = BotState.Idle;
                    }*/
                    break;
            }

        }
        else if (!playerController.isControlled && playerManager.onCatch)
        {
            idle();
        }


        /*  if (target != null)
          {
              UpdatePath();
          }
          else if (target == null)
          {
              target = botCom.GetTargetPos(2, playerManager);
          }*/



    }

    private void idle()
    {
        agent.speed = 0f;
        animator.SetFloat("Speed", 0);
        agent.isStopped = true;
        target = null;
        playerAnimation.rig.weight = 0;

    }

    private void goBase()
    {
        agent.isStopped = false;

        agent.SetDestination(target.position);
        animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);
        if (agent.remainingDistance <= 8f)
        {
            simulateTouch();

            if (playerManager.playerTimer < 1f)
            {
                currentState = BotState.Idle;
            }
        }
    }

    private void ChasingPlayer()
    {
      //  if (!playerManager.onSaveZone) { }
        agent.SetDestination(target.position);
        animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);
        agent.isStopped = false;

        if (agent.remainingDistance <= 5)
        {
            simulateTouch();

            if (otherPlayerWare.gotPlayer)
            {
                currentState = BotState.Idle;
                otherPlayerWare.gotPlayer = false;

            }
        }
        if (!playerManager.onSaveZone && distance < 20)
        {
            currentState = BotState.BackToBase;
        }
    }

    private void walkRandom()
    {
        agent.isStopped = false;
        int r = Random.Range(1, 5);
        target = gameManager.points[r];
        agent.SetDestination(target.position);
        animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);

        if (agent.remainingDistance <= 1)
        {
            currentState = BotState.Idle;
        }

    }



    public void checkplayer(PlayerManager otherplayer)
    {
        if(otherplayer.playerTimer > playerManager.playerTimer)
        {
            if (readyFight)
            {
                currentState = BotState.ChasingEnemy;
                target = otherplayer.transform;
            }
           
        }
        else if (otherplayer.playerTimer < playerManager.playerTimer)
        {
             distance = Vector3.Distance(otherplayer.transform.position, playerManager.transform.position);
        }
    }
    public float pathUpdateDelay = 0.2f;
   


    void simulateTouch()
    {

        bool onRange = Vector3.Distance(transform.position, target.position )<= 8;
        if (onRange && !playerController.isControlled )
        {

            
            playerAnimation.rig.weight = 1;
            playerController.handrise = true;
        }
        else 
        {
            playerAnimation.rig.weight = 0;
            playerController.handrise = false;
        }
    }
}

/*


 if (!playerController.isControlled && !playerController.CheckOnCatcth()&& !onAvoidingPlayer)
                {
                    agent.enabled = true;
                    switch (currentState)
                    {
                        case BotState.AttackTarget:
                        //if player on savezpne touch base first then chase the enemy
                            UpdatePath();
                            
                            break;
                        case BotState.DefendTheBase:
                        UpdatePath(); //-> going to base

                        //if player arrived stop (step on SavedZone) and standby() looking for target? using raycast?


                        // Logic to defend the base
                        break;
                        case BotState.standby:
                            // avoid target false
                            // Logic to run from the target with lower time
                            // if theres player found send request to empty id state Player
                            // touch base
                        //chase enemy
                            break;
                        case BotState.AttackBase:
                            // Logic to attack the enemy base
                            //raycast on watching the higher player :: -> avoid target
                            break;
                        case BotState.AvoidTarget:
                            // Logic to avoid the target (if raycast detect enemy had high timer)
                            // avoid variable be true
                            // set target 
                            // run to the base
                            // raycast 
                            break;
                        case BotState.releaseFriend:
                        //if total team player on catach > 2 go to release firnd someone who doesnt defend or random bot if all defending
                            // Logic to release a friend
                            break;
                        default:
                            Debug.LogWarning("Unhandled BotState: " + currentState);
                            break;
                    }
                }
                else
                {
                    //or make function to stop AI
                    agent.enabled = false;
                    agent.speed = 0;
                    agent.destination = transform.position;
                }


 private void UpdatePath()
    {
        
            agent.SetDestination(target.position);

            animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);
            if (agent.remainingDistance <= 5 && !agent.pathPending &&!agent.isStopped)
            {
                simulateTouch();
                if(playerManager.gotPlayer)
                {
                    agent.isStopped = true;
                  //  botCom.switchDefend(playerManager.idPlayer, playerManager.team, playerManager);
                  //  playerManager.gotPlayer = false;
                    agent.isStopped = false;

                }

                if (playerManager.playerTimer < 1f)
                {
                    
                    playerController.handrise = false;

                    agent.isStopped = true;
                    agent.speed = 0f;
                   target = botCom.GetTargetPos(4,playerManager);
                    playerAnimation.rig.weight = 0;
                    Debug.Log("no");
                   
                    animator.SetFloat("Speed", 0);

                    //standby()
                }


            }
        
        
    }
*/