using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCounterVisual : MonoBehaviour
{
    private IngredientCounter ingredientCounter;
    private Animator animator;
    private const string OPEN_CLOSE_TRIGGER = "OpenClose";
    private static readonly int OpenCloseTriggerID = Animator.StringToHash(OPEN_CLOSE_TRIGGER);

    private void Awake()
    {
        ingredientCounter = GetComponent<IngredientCounter>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ingredientCounter.OnPlayerPickedIngredient += IngredientCounter_OnPlayerPickedIngredient;
    }

    private void IngredientCounter_OnPlayerPickedIngredient(object sender, EventArgs e)
    {
        animator.SetTrigger(OpenCloseTriggerID);
    }
}
