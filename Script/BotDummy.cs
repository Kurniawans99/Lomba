using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BotState
{
    AttackTarget,
    DefendTheBase,
    RunFromTarget,
    AttackBase,
    AvoidTarget,
    releaseFriend,


}
public class BotDummy : MonoBehaviour
{
    public BotState currentState;
    private NavMeshAgent agent;
    private Animator animator;
    private PlayerController playerController;

    public Transform target;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(target != null)
        {
            if (!playerController.isControlled && !playerController.CheckOnCatcth()) 
            {
                agent.enabled = true;
                switch (currentState)
                {
                    case BotState.AttackTarget:
                        UpdatePath();
                        simulateTouch();
                        break;
                    case BotState.DefendTheBase:
                        UpdatePath();
                        // Logic to defend the base
                        break;
                    case BotState.RunFromTarget:
                        // Logic to run from the target with lower time
                        break;
                    case BotState.AttackBase:
                        // Logic to attack the enemy base
                        break;
                    case BotState.AvoidTarget:
                        // Logic to avoid the target
                        break;
                    case BotState.releaseFriend:
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
