using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradePanelUI : MonoBehaviour
{
    [SerializeField] private UpgradeItemUI upgradeItemStove;
    [SerializeField] private List<FryingRecipeSO> fryingRecipeSOList;

    private void Start()
    {
        upgradeItemStove.GetUpgradeButton().onClick.AddListener(OnUpgradeStoveButtonClicked);
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
        }
    }
}
