using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
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
        //shopPanel.SetActive(true);
        shopPanel.GetComponent<ShopPanelUIVisual>().Open();
        Time.timeScale = 0f;

        foreach (IngredientItemUI item in ingredientItemList)
        {
            item.UpdateText();
        }

        moneyText.text = ResourceManager.Instance.GetMoney().ToString();
    }
    
    private void OnCloseShopButtonClicked()
    {
        //shopPanel.SetActive(false);
        shopPanel.GetComponent<ShopPanelUIVisual>().Close();
        Time.timeScale = 1f;
    }
    
    private void OnChangePanelButtonClicked()
    {
        buyIngredientsPanel.SetActive(!buyIngredientsPanel.activeInHierarchy);
        upgradePanel.SetActive(!upgradePanel.activeInHierarchy);
    }
}
