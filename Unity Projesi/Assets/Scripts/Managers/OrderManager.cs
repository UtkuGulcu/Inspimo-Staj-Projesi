using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
   
    public static OrderManager Instance { get; private set; }

    [SerializeField] private List<RecipeSO> easyDifficultyRecipeList;
    [SerializeField] private List<RecipeSO> mediumDifficultyRecipeList;
    [SerializeField] private List<RecipeSO> hardDifficultyRecipeList;

    private List<RecipeSO> availableRecipeList;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are multiple Order Managers!!!");
            Destroy(this);
        }
    }

    private void Start()
    {
        availableRecipeList = easyDifficultyRecipeList;
        DifficultyManager.Instance.OnDifficultyChanged += DifficultyManager_OnDifficultyChanged;
    }

    private void OnDisable()
    {
        DifficultyManager.Instance.OnDifficultyChanged -= DifficultyManager_OnDifficultyChanged;
    }

    private void DifficultyManager_OnDifficultyChanged(object sender, DifficultyManager.OnDifficultyChangedEventArgs e)
    {
        availableRecipeList = e.newDifficulty == DifficultyManager.Difficulty.Medium ? mediumDifficultyRecipeList : hardDifficultyRecipeList;
    }


    public RecipeSO GetRandomRecipe()
    {
        int randomIndex = Random.Range(0, availableRecipeList.Count);
        return availableRecipeList[randomIndex];
    }
}
