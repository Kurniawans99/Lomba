using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BotState
{
    AttackTarget,
    DefendTheBase,
    standby,
    AttackBase,
    AvoidTarget,
    releaseFriend,
    botRelease,



}

/*
 * first all bot going to defend then set bot communication to initial the team
2 player attack base but if theres enemy had higher point one should be avoid player, 
and should be idle wait until the player comming to warn distand
player who got in savezone going to attack the base & player who got warn distance comming to defendbase
 */
public class BotDummy : MonoBehaviour
{
    public BotState currentState;
    private NavMeshAgent agent;
    private Animator animator;
    private PlayerController playerController;
    public bool onAvoidingPlayer = false;

    public Transform target;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        //check if theres a people with high team near from here
        
            if (target != null)
            {
                if (!playerController.isControlled && !playerController.CheckOnCatcth()&& !onAvoidingPlayer)
                {
                    agent.enabled = true;
                    switch (currentState)
                    {
                        case BotState.AttackTarget:
                        //if player on savezpne touch base first then chase the enemy
                            UpdatePath();
                            simulateTouch();
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
            }
        
       
      

        
    }

    private void Standby()
    {
        //raycast watching the target is greater? and update current state to attack player while player defending

    }
    private void onWarn()
    {

    }

    public float pathUpdateDelay = 0.2f;
    private void UpdatePath()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);
            
        }
        
    }


    void simulateTouch()
    {
        bool onRange = Vector3.Distance(transform.position, target.position )<= 3;
        if (onRange)
        {
            //simulate Touch
        }
    }
}
