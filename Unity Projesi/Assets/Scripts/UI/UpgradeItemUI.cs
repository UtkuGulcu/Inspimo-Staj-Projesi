using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private int[] upgradePrices;
    [SerializeField] private int[] upgradePercentages;
    [SerializeField] private TMP_Text upgradeButtonText;
    [SerializeField] private TMP_Text upgradePercentageText;

    private int upgradeCount;
    
    public Button GetUpgradeButton()
    {
        return upgradeButton;
    }

    public int GetUpgradePrice()
    {
        return upgradePrices[upgradeCount];
    }

    public void IncreaseUpgradeCount()
    {
        upgradeCount++;
        
        if (upgradeCount == 3)
        {
            Destroy(gameObject);
            return;
        }
        
        upgradeButtonText.text = $"Upgrade {upgradePrices[upgradeCount]}$";
        upgradePercentageText.text = $"%{upgradePercentages[upgradeCount]}";
    }
}
