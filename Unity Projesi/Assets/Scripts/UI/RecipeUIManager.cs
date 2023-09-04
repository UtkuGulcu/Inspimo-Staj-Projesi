using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUIManager : MonoBehaviour
{
    [SerializeField] private GameObject recipeTemplate;

    private Table[] tableArray;
    private List<RecipeUISingle> recipeUISingleList;

    private void Start()
    {
        recipeUISingleList = new List<RecipeUISingle>();
        tableArray = FindObjectsByType<Table>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        foreach (Table table in tableArray)
        {
            table.OnRecipeOrdered += Table_OnRecipeOrdered;
            table.OnRecipeDone += Table_OnRecipeDone;
        }
    }

    private void Table_OnRecipeOrdered(object sender, Table.OnRecipeOrderedEventArgs e)
    {
        GameObject spawnedRecipe = Instantiate(recipeTemplate, transform);
        RecipeUISingle recipeUISingle = spawnedRecipe.GetComponent<RecipeUISingle>();
        recipeUISingle.SetupVisuals(e.recipeSO);
        recipeUISingleList.Add(recipeUISingle);
    }
    
    private void Table_OnRecipeDone(object sender, Table.OnRecipeDoneEventArgs e)
    {
        foreach (RecipeUISingle recipeUISingle in recipeUISingleList)
        {
            if (recipeUISingle.GetRecipeSO() == e.recipeSO)
            {
                recipeUISingleList.Remove(recipeUISingle);
                recipeUISingle.DestroySelf();
                return;
            }
        }
    }
}
