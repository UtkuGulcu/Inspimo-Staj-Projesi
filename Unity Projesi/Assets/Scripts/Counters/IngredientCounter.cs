using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientCounter : Counter
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    
    private int stockCount;

    protected override void Start()
    {
        base.Start();
        stockCount = 20;
    }

    public override void Interact()
    {
        if (!Player.Instance.HasKitchenObject() && stockCount > 0)
        {
            KitchenObject.SpawnKitchenObject(KitchenObjectSO, Player.Instance);
            stockCount--;
        }
    }
}
