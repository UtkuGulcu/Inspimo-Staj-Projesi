using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : Counter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private KitchenObject kitchenObjectPlate;

    private void Start()
    {
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
        kitchenObjectPlate = GetKitchenObject();
    }

    public override void Interact(Player interactedPlayer)
    {
        KitchenObject kitchenObjectPlayer = interactedPlayer.GetKitchenObject();
        
        if (kitchenObjectPlayer == null)
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, interactedPlayer);
        }
        else if (kitchenObjectPlate.GetComponent<Plate>().TryToAddToPlate(kitchenObjectPlayer.GetKitchenObjectSO()))
        {
            kitchenObjectPlayer.DestroySelf();
            kitchenObjectPlate.SetKitchenObjectParent(interactedPlayer);
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
            kitchenObjectPlate = GetKitchenObject();
        }
    }
}
