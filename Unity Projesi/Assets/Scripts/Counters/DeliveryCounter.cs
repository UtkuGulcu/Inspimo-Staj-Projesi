using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : Counter
{
    public override void Interact(Player interactedPlayer)
    {
        KitchenObject kitchenObjectPlayer = interactedPlayer.GetKitchenObject();
        bool isPlayerHoldingKitchenObject = kitchenObjectPlayer != null;

        if (!HasKitchenObject() && kitchenObjectPlayer is Plate)
        {
            kitchenObjectPlayer.SetKitchenObjectParent(this);
            return;
        }

        if (HasKitchenObject() && !isPlayerHoldingKitchenObject)
        {
            GetKitchenObject().SetKitchenObjectParent(interactedPlayer);
            return;
        }

        if (HasKitchenObject() && GetKitchenObject().GetComponent<Plate>()
                .TryToAddToPlate(kitchenObjectPlayer.GetKitchenObjectSO()))
        {
            kitchenObjectPlayer.DestroySelf();
            GetKitchenObject().SetKitchenObjectParent(interactedPlayer);
        }
    }
}
