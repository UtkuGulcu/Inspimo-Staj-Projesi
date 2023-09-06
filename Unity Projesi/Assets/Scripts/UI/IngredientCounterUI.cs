using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientCounterUI : MonoBehaviour
{
    [SerializeField] private IngredientCounter ingredientCounter;
    [SerializeField] private TMP_Text stockText;
    
    private IngredientCounter[] ingredientCounterArray;

    private void Start()
    {
        transform.forward = Camera.main.transform.forward;
        ResourceManager.Instance.OnKitchenObjectResourceChanged += KitchenObjectResourceManagerOnKitchenObjectResourceChanged;
        UpdateText();
    }

    private void KitchenObjectResourceManagerOnKitchenObjectResourceChanged(object sender, EventArgs e)
    {
        UpdateText();
    }

    private void UpdateText()
    {
        stockText.text = ResourceManager.Instance.GetResourceAmount(ingredientCounter.GetIngredient()).ToString();
    }
}
