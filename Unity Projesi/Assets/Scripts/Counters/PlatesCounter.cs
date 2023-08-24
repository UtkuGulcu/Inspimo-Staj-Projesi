using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : Counter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private KitchenObject kitchenObjectPlate;

    protected override void Start()
    {
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
        kitchenObjectPlate = GetKitchenObject();
    }

    public override void Interact()
    {
        KitchenObject kitchenObjectPlayer = Player.Instance.GetKitchenObject();
        
        if (kitchenObjectPlayer == null)
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, Player.Instance);
        }
        else if (kitchenObjectPlate.GetComponent<Plate>().TryToAddToPlate(kitchenObjectPlayer.GetKitchenObjectSO()))
        {
            kitchenObjectPlayer.DestroySelf();
            kitchenObjectPlate.SetKitchenObjectParent(Player.Instance);
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
            kitchenObjectPlate = GetKitchenObject();
        }
    }
}
