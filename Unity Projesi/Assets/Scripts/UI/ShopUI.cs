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
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private List<IngredientItemUI> ingredientItemList;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        openShopButton.onClick.AddListener(OnOpenShopButtonClicked);
        closeShopButton.onClick.AddListener(OnCloseShopButtonClicked);
        ResourceManager.Instance.OnMoneyChanged += ResourceManager_OnMoneyChanged;
    }

    private void ResourceManager_OnMoneyChanged(object sender, ResourceManager.OnMoneyChangedEventArgs e)
    {
        moneyText.text = e.newMoneyAmount.ToString();
    }

    private void OnOpenShopButtonClicked()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0f;

        foreach (IngredientItemUI item in ingredientItemList)
        {
            item.UpdateText();
        }

        moneyText.text = ResourceManager.Instance.GetMoney().ToString();
    }
    
    private void OnCloseShopButtonClicked()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
