using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static event EventHandler OnAnyChangePanelButtonDown;
    
    [SerializeField] private Button openShopButton;
    [SerializeField] private Button closeShopButton;
    [SerializeField] private Button changePanelButton;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject buyIngredientsPanel;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private List<IngredientItemUI> ingredientItemList;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        openShopButton.onClick.AddListener(OnOpenShopButtonClicked);
        closeShopButton.onClick.AddListener(OnCloseShopButtonClicked);
        changePanelButton.onClick.AddListener(OnChangePanelButtonClicked);
        ResourceManager.Instance.OnMoneyChanged += ResourceManager_OnMoneyChanged;
    }

    private void ResourceManager_OnMoneyChanged(object sender, ResourceManager.OnMoneyChangedEventArgs e)
    {
        moneyText.text = e.newMoneyAmount.ToString();
    }

    private void OnOpenShopButtonClicked()
    {
        shopPanel.GetComponent<PanelUIVisual>().Open();
        Time.timeScale = 0f;

        foreach (IngredientItemUI item in ingredientItemList)
        {
            item.UpdateText();
        }

        moneyText.text = ResourceManager.Instance.GetResourceAmount(ResourceManager.ResourceType.Money).ToString();
        OnAnyChangePanelButtonDown?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnCloseShopButtonClicked()
    {
        shopPanel.GetComponent<PanelUIVisual>().Close();
        Time.timeScale = 1f;
        OnAnyChangePanelButtonDown?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnChangePanelButtonClicked()
    {
        buyIngredientsPanel.SetActive(!buyIngredientsPanel.activeInHierarchy);
        upgradePanel.SetActive(!upgradePanel.activeInHierarchy);
        OnAnyChangePanelButtonDown?.Invoke(this, EventArgs.Empty);
    }
}
