using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUIVisual : MonoBehaviour
{
    private Animator animator;
    private static readonly int CloseTriggerID = Animator.StringToHash("close");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        animator.SetTrigger(CloseTriggerID);
    }

    // Animation event for close animation
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
