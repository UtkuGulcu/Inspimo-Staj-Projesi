using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradePanelUI : MonoBehaviour
{
    public static event EventHandler OnUpgradeButtonClicked;

    [SerializeField] private UpgradeItemUI upgradeItemStove;
    [SerializeField] private UpgradeItemUI upgradeItemMovementSpeed;
    [SerializeField] private List<FryingRecipeSO> fryingRecipeSOList;

    private void Start()
    {
        upgradeItemStove.GetUpgradeButton().onClick.AddListener(OnUpgradeStoveButtonClicked);
        upgradeItemMovementSpeed.GetUpgradeButton().onClick.AddListener(OnUpgradeMovementSpeedButtonClicked);
    }

    private void OnUpgradeStoveButtonClicked()
    {
        if (ResourceManager.Instance.GetMoney() >= upgradeItemStove.GetUpgradePrice())
        {
            ResourceManager.Instance.DecreaseMoney(upgradeItemStove.GetUpgradePrice());
            upgradeItemStove.IncreaseUpgradeCount();

            foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOList)
            {
                Debug.Log($"Old Frying Timer: {fryingRecipeSO.fryingTimerMax}");
                fryingRecipeSO.fryingTimerMax = fryingRecipeSO.fryingTimerMax * 90 / 100;
                Debug.Log($"New Frying Timer: {fryingRecipeSO.fryingTimerMax}");
            }
            
            OnUpgradeButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
    
    private void OnUpgradeMovementSpeedButtonClicked()
    {
        if (ResourceManager.Instance.GetMoney() >= upgradeItemMovementSpeed.GetUpgradePrice())
        {
            ResourceManager.Instance.DecreaseMoney(upgradeItemMovementSpeed.GetUpgradePrice());
            upgradeItemMovementSpeed.IncreaseUpgradeCount();

            PlayerController[] playerControllerArray = FindObjectsByType<PlayerController>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (PlayerController playerController in playerControllerArray)
            {
                Debug.Log($"Old MovementSpeed: {playerController.GetMovementSpeed()}");
                float oldMovementSpeed = playerController.GetMovementSpeed();
                oldMovementSpeed *= 1.1f;
                playerController.SetMovementSpeed(oldMovementSpeed);
                Debug.Log($"New MovementSpeed: {playerController.GetMovementSpeed()}");
            }
            
            OnUpgradeButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
