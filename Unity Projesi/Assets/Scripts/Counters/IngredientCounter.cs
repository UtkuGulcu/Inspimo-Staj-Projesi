using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientCounter : Counter
{
    public event EventHandler OnPlayerPickedIngredient;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    private int stockCount;

    protected override void Start()
    {
        base.Start();
        stockCount = 20;
    }

    public override void Interact(Player interactedPlayer)
    {
        if (stockCount <= 0)
        {
            return;
        }

        KitchenObject kitchenObjectPlayer = interactedPlayer.GetKitchenObject();

        if (kitchenObjectPlayer == null)
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, interactedPlayer);
            stockCount--;
            OnPlayerPickedIngredient?.Invoke(this, EventArgs.Empty);
        }
        else if (kitchenObjectPlayer.GetKitchenObjectSO() == kitchenObjectSO) 
        {
            kitchenObjectPlayer.DestroySelf();
            stockCount++;
            OnPlayerPickedIngredient?.Invoke(this, EventArgs.Empty);
        }
        else if (kitchenObjectPlayer.TryGetComponent(out Plate plate))
        {
            if (!plate.TryToAddToPlate(kitchenObjectSO)) return;
            
            stockCount--;
            OnPlayerPickedIngredient?.Invoke(this, EventArgs.Empty);
        }
    }
}
