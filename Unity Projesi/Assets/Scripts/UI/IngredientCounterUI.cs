using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientCounterUI : MonoBehaviour
{
    [SerializeField] private IngredientCounter ingredientCounter;
    [SerializeField] private TMP_Text stockText;

    private void Start()
    {
        transform.forward = Camera.main.transform.forward;
        ingredientCounter.OnPlayerPickedIngredient += IngredientCounter_OnPlayerPickedIngredient;
        UpdateText();
    }

    private void IngredientCounter_OnPlayerPickedIngredient(object sender, EventArgs e)
    {
        UpdateText();
    }

    private void UpdateText()
    {
        stockText.text = ResourceManager.Instance.GetResourceAmount(ingredientCounter.GetIngredient()).ToString();
    }
}
