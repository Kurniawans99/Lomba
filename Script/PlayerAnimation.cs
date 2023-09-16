using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private const string WALK = "Walk";
    private const string RUN = "Run";
    private Animator animator;
    private float acceleration = 0.1f;
    string tagName = "rigHand";

    public GameObject objectFind;
    private Rig rig;

    private void Start()
    {
        rig = GetComponentInChildren<Rig>();
        player = GetComponentInParent<PlayerController>();
      
    }



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (player.handrise)
        {
            rig.weight = 1;
        }
        else
        {
            rig.weight = 0;

        }

        if (player.IsMoving() != Vector3.zero)
        {
            if (player.IsRunning())
            {
                animator.SetBool(WALK, false);
                animator.SetBool(RUN, true);
            }
            else
            {
                animator.SetBool(WALK, true);
                animator.SetBool(RUN, false);
            }
        }
        else
        {
            animator.SetBool(WALK, false);
            animator.SetBool(RUN, false);
        }

       


    }


}
