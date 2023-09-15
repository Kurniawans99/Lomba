using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private const string WALK = "Walk";
    private const string RUN = "Run";
    private Animator animator;
    private float acceleration = 0.1f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(player.IsMoving() != Vector3.zero && !player.IsRunning())
        {
            
            animator.SetBool(WALK, true);
        }
        else 
        {
            animator.SetBool(WALK, false);
        }

        if (player.IsRunning()){
            animator.SetBool(RUN, true);
        }
        else
        {
            animator.SetBool(RUN, false);
        }
    }


}
