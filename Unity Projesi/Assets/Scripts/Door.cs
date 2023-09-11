using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static Door Instance { get; private set; }

    public event EventHandler OnDoorOpened;
    public event EventHandler OnDoorClosed;
    
    private static readonly int OpenTriggerID = Animator.StringToHash("open");
    private static readonly int CloseTriggerID = Animator.StringToHash("close");

    private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is more than one door");
            Destroy(this);
        }
        
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger(OpenTriggerID);
        OnDoorOpened?.Invoke(this, EventArgs.Empty);
    }
    
    public void CloseDoor()
    {
        animator.SetTrigger(CloseTriggerID);
    }

    public void PlayDoorCloseSoundAnimationEvent()
    {
        OnDoorClosed?.Invoke(this, EventArgs.Empty);
    }
}
