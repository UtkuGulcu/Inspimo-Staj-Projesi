using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : Counter, IAlternateInteractable, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    [SerializeField] private List<CuttingRecipeSO> validCuttingRecipes;

    private CuttingRecipeSO cuttingRecipeSO;
    private int cuttingProgress;
    
    public override void Interact()
    {
        KitchenObject kitchenObjectPlayer = Player.Instance.GetKitchenObject();

        if (!HasKitchenObject())
        {
            if (HasRecipeWithInput(kitchenObjectPlayer.GetKitchenObjectSO()))
            {
                Player.Instance.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingRecipeSO = GetCuttingRecipeWithInput(kitchenObjectPlayer.GetKitchenObjectSO());
            }
        }
        else
        {
            if (kitchenObjectPlayer == null)
            {
                GetKitchenObject().SetKitchenObjectParent(Player.Instance);
            }
            else if (kitchenObjectPlayer.TryGetComponent(out Plate playerPlate))
            {
                if (playerPlate.TryToAddToPlate(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestroySelf();
                }
            }
            else if (HasRecipeWithInput(kitchenObjectPlayer.GetKitchenObjectSO()))
            {
                KitchenObject oldKitchenObjectPlayer = Player.Instance.GetKitchenObject();
                oldKitchenObjectPlayer.SetParentNull();
                GetKitchenObject().SetKitchenObjectParent(Player.Instance);
                oldKitchenObjectPlayer.SetKitchenObjectParent(this);
                cuttingRecipeSO = GetCuttingRecipeWithInput(oldKitchenObjectPlayer.GetKitchenObjectSO());
            }
        }
    }
    
    public void AlternateInteract()
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            InvokeOnProgressChangedEvent((float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax);

            if (cuttingProgress == cuttingRecipeSO.cuttingProgressMax)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(cuttingRecipeSO.output, this);
                cuttingProgress = 0;
                InvokeOnProgressChangedEvent(0f);
            }
        }
    }
    
    private CuttingRecipeSO GetCuttingRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO recipe in validCuttingRecipes)
        {
            if (recipe.input == kitchenObjectSO)
            {
                return recipe;
            }
        }

        return null;
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO _cuttingRecipeSO = GetCuttingRecipeWithInput(kitchenObjectSO);
        return _cuttingRecipeSO != null;
    }

    private void InvokeOnProgressChangedEvent(float newProgressNormalized)
    {
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = newProgressNormalized
        });
    }
}
