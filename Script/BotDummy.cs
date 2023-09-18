using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BotState
{
    RunTheTarget,
    DefendTheBase,
    RunFromTarget,
    AttackBase,
    AvoidTargetOutsideBase
}
public class BotDummy : MonoBehaviour
{
    public BotState currentState;
    public NavMeshAgent agent;
    public Animator animator;
    private PlayerController playerController;

    public Transform target;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerController.isControlled)
        {
            switch (currentState)
            {
                case BotState.RunTheTarget:
                    // Logic to move towards the target with higher 
                    break;
                case BotState.DefendTheBase:
                    // Logic to defend the base
                    break;
                case BotState.RunFromTarget:
                    // Logic to run from the target with lower time
                    break;
                    // Add other cases for different states...
            }
        }

        
    }

    private void getTargetPos(int who)
    {
       /* 1. Enemy Base
        2. Ally Base
        3. Enmy human*/
       if (who == 1)
        {

        }
        else if (who == 2)
        {

        }
        else if(who == 3)
        {

        }
    }

}
