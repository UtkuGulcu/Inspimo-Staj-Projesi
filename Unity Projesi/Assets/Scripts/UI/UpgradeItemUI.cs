using System;
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

    private void Start()
    {
        SetText();
    }

    public Button GetUpgradeButton()
    {
        return upgradeButton;
    }

    public int GetUpgradePrice()
    {
        return upgradePrices[upgradeCount];
    }

    public int GetUpgradeCount()
    {
        return upgradeCount;
    }
    
    public void SetUpgradeCount(int newUpgradeCount)
    {
        upgradeCount = newUpgradeCount;
    }

    public void IncreaseUpgradeCount()
    {
        upgradeCount++;
        
        if (upgradeCount == 3)
        {
            DestroySelf();
            return;
        }
        
        SetText();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void SetText()
    {
        upgradeButtonText.text = $"Upgrade {upgradePrices[upgradeCount]}$";
        upgradePercentageText.text = $"%{upgradePercentages[upgradeCount]}";
    }
}
