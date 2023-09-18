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
        if (player.isControlled)
        {
            float speed = player.IsMoving() != Vector3.zero ? (player.IsRunning() ? 1.0f : 0.5f) : 0f;

            animator.SetFloat("Speed", speed);

            if (player.handrise)
            {
                rig.weight = 1;
            }
            else
            {
                rig.weight = 0;

            }
        }
     


       


    }


}
