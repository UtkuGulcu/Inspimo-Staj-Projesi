using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    public static FixedJoystick Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are more than one joystick");
        }
    }
}