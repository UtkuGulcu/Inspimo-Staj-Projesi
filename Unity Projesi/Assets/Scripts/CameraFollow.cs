using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private enum State
    {
        Locked,
        Follow
    }
    
    [SerializeField] private GameObject playerWaiter;
    [SerializeField] private Vector3 kitchenScreenLockedPosition;

    private State state;

    private void Start()
    {
        ControlButtonsUI.Instance.OnSwitchCharacterButtonDown += UIManager_OnSwitchCharacterButtonDown;
        state = State.Follow;
    }

    private void OnDisable()
    {
        ControlButtonsUI.Instance.OnSwitchCharacterButtonDown -= UIManager_OnSwitchCharacterButtonDown;
    }

    private void UIManager_OnSwitchCharacterButtonDown(object sender, EventArgs e)
    {
        if (state == State.Locked)
        {
            state = State.Follow;
        }
        else
        {
            state = State.Locked;
            transform.position = kitchenScreenLockedPosition;
        }
    }

    private void LateUpdate()
    {
        switch (state)
        {
            case State.Locked:
                break;
            case State.Follow:
                transform.position = playerWaiter.transform.position + kitchenScreenLockedPosition;
                break;
        }
    }
}
