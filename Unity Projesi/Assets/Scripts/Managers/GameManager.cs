using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum ActiveCharacter
    {
        Chef,
        Waiter
    }

    [SerializeField] private Player chef;
    [SerializeField] private Player waiter;

    private ActiveCharacter activeCharacter;
    
    private void Start()
    {
        ControlButtonsUI.Instance.OnSwitchCharacterButtonDown += UIManager_OnSwitchCharacterButtonDown;
        activeCharacter = ActiveCharacter.Waiter;
    }

    private void OnDisable()
    {
        ControlButtonsUI.Instance.OnSwitchCharacterButtonDown -= UIManager_OnSwitchCharacterButtonDown;
    }

    private void UIManager_OnSwitchCharacterButtonDown(object sender, EventArgs e)
    {
        if (activeCharacter == ActiveCharacter.Chef)
        {
            chef.DisablePlayer();
            waiter.EnablePlayer();
            activeCharacter = ActiveCharacter.Waiter;
        }
        else
        {
            activeCharacter = ActiveCharacter.Chef;
            waiter.DisablePlayer();
            chef.EnablePlayer();
        }
    }
}
