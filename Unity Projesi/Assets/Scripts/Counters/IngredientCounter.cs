using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientCounter : Counter
{
    public event EventHandler OnPlayerPickedIngredient;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player interactedPlayer)
    {
        if (!ResourceManager.Instance.IsKitchenObjectAvailable(kitchenObjectSO))
        {
            return;
        }

        KitchenObject kitchenObjectPlayer = interactedPlayer.GetKitchenObject();

        if (kitchenObjectPlayer == null)
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, interactedPlayer);
            ResourceManager.Instance.DecreaseKitchenObjectAmount(kitchenObjectSO, 1);
            OnPlayerPickedIngredient?.Invoke(this, EventArgs.Empty);
        }
        else if (kitchenObjectPlayer.GetKitchenObjectSO() == kitchenObjectSO) 
        {
            kitchenObjectPlayer.DestroySelf();
            ResourceManager.Instance.IncreaseKitchenObjectAmount(kitchenObjectSO, 1);
            OnPlayerPickedIngredient?.Invoke(this, EventArgs.Empty);
        }
        else if (kitchenObjectPlayer.TryGetComponent(out Plate plate))
        {
            if (!plate.TryToAddToPlate(kitchenObjectSO)) return;
            
            ResourceManager.Instance.DecreaseKitchenObjectAmount(kitchenObjectSO, 1);
            OnPlayerPickedIngredient?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObjectSO GetIngredient()
    {
        return kitchenObjectSO;
    }
}
