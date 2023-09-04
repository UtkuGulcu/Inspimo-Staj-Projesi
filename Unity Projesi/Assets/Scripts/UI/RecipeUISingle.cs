using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUISingle : MonoBehaviour
{
    [SerializeField] private TMP_Text recipeNameText;
    [SerializeField] private Transform ingredientIconsParent;
    [SerializeField] private Transform iconTemplate;

    private RecipeSO recipeSO;
    
    public void SetupVisuals(RecipeSO _recipeSO)
    {
        recipeSO = _recipeSO;
        gameObject.SetActive(true);
        recipeNameText.text = recipeSO.Name;

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Image image = Instantiate(iconTemplate, ingredientIconsParent).GetComponent<Image>();
            image.sprite = kitchenObjectSO.icon;
            image.gameObject.SetActive(true);
        }
    }

    public RecipeSO GetRecipeSO()
    {
        return recipeSO;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
