using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject kitchenCamera;
    [SerializeField] private GameObject restaurantCamera;

    private void Start()
    {
        ControlButtonsUI.Instance.OnSwitchCharacterButtonDown += UIManager_OnSwitchCharacterButtonDown;
        restaurantCamera.SetActive(true);
        kitchenCamera.SetActive(false);
    }

    private void OnDisable()
    {
        ControlButtonsUI.Instance.OnSwitchCharacterButtonDown -= UIManager_OnSwitchCharacterButtonDown;
    }

    private void UIManager_OnSwitchCharacterButtonDown(object sender, EventArgs e)
    {
        kitchenCamera.SetActive(!kitchenCamera.activeInHierarchy);
        restaurantCamera.SetActive(!restaurantCamera.activeInHierarchy);
    }
}
