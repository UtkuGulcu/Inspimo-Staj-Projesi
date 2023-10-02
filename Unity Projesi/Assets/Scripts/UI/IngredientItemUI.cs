using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientItemUI : MonoBehaviour
{
    public static event EventHandler OnIngredientBought;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private Button buyButton;

    private void Start()
    {
        buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnBuyButtonClicked()
    {
        ResourceManager resourceManager = ResourceManager.Instance;
        
        if (resourceManager.GetResourceAmount(ResourceManager.ResourceType.Money) >= kitchenObjectSO.price)
        {
            resourceManager.IncreaseKitchenObjectAmount(kitchenObjectSO, 1);
            resourceManager.DecreaseMoney(kitchenObjectSO.price);
            UpdateText();    
        }
        
        OnIngredientBought?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateText()
    {
        amountText.text = ResourceManager.Instance.GetResourceAmount(kitchenObjectSO).ToString();
    }
}
