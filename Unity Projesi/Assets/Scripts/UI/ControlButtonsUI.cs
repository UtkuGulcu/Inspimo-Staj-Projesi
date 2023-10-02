using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButtonsUI : MonoBehaviour
{
    public static ControlButtonsUI Instance { get; private set; }

    public event EventHandler OnInteractButtonDown;
    public event EventHandler OnAlternateInteractButtonDown;
    public event EventHandler OnSwitchCharacterButtonDown;

    [SerializeField] private Button InteractButton;
    [SerializeField] private Button AlternateInteractButton;
    [SerializeField] private Button SwitchCharacterButton;

    private void Awake()
    {

        #region Singleton

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are more than one UI Managers");
        }

        #endregion
    }

    private void Start()
    {
        InteractButton.onClick.AddListener(() => OnInteractButtonDown?.Invoke(this, EventArgs.Empty));
        AlternateInteractButton.onClick.AddListener(() => OnAlternateInteractButtonDown?.Invoke(this, EventArgs.Empty));
        SwitchCharacterButton.onClick.AddListener(() => OnSwitchCharacterButtonDown?.Invoke(this, EventArgs.Empty));
    }
    
    public bool HasEventListeners()
    {
        return OnInteractButtonDown != null;
    }
}
