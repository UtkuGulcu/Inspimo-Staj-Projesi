using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerVisual : MonoBehaviour
{
    private Animator animator;
    private static readonly int IsWalkingID = Animator.StringToHash("isWalking");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartMoving()
    {
        animator.SetBool(IsWalkingID, true);
    }
    
    public void StopMoving()
    {
        animator.SetBool(IsWalkingID, false);
    }
}
