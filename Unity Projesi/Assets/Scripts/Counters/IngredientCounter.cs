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

    public override void Interact()
    {
        if (stockCount <= 0)
        {
            return;
        }
        
        if (!Player.Instance.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, Player.Instance);
            stockCount--;
            OnPlayerPickedIngredient?.Invoke(this, EventArgs.Empty);
        }
        else if (Player.Instance.GetKitchenObject().GetKitchenObjectSO() == kitchenObjectSO) 
        {
            Player.Instance.GetKitchenObject().DestroySelf();
            stockCount++;
            OnPlayerPickedIngredient?.Invoke(this, EventArgs.Empty);
        }
        else if (Player.Instance.GetKitchenObject().TryGetComponent(out Plate plate))
        {
            plate.TryToAddToPlate(kitchenObjectSO);
        }
    }
}
