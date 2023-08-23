using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private const string IS_WALKING = "isWalking"; 
        
    private PlayerController playerController;
    private Animator animator;
    private static readonly int IS_WALKING_ID = Animator.StringToHash(IS_WALKING);

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING_ID, playerController.IsWalking());
    }
}