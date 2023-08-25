using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : Counter
{
    public override void Interact(Player interactedPlayer)
    {
        KitchenObject kitchenObjectPlayer = interactedPlayer.GetKitchenObject();
        
        if (!HasKitchenObject())
        {
            if (kitchenObjectPlayer != null)
            {
                kitchenObjectPlayer.SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (kitchenObjectPlayer == null)
            {
                GetKitchenObject().SetKitchenObjectParent(interactedPlayer);
            }
            else if (kitchenObjectPlayer.TryGetComponent(out Plate playerPlate))
            {
                if (playerPlate.TryToAddToPlate(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestroySelf();
                }
            }
            else if (GetKitchenObject().TryGetComponent(out Plate counterPlate))
            {
                if (counterPlate.TryToAddToPlate(kitchenObjectPlayer.GetKitchenObjectSO()))
                {
                    kitchenObjectPlayer.DestroySelf();
                    GetKitchenObject().SetKitchenObjectParent(interactedPlayer);
                }
            }
            else
            {
                KitchenObject oldKitchenObject = kitchenObjectPlayer;
                oldKitchenObject.SetParentNull();
                GetKitchenObject().SetKitchenObjectParent(interactedPlayer);
                oldKitchenObject.SetKitchenObjectParent(this);
            }
        }
    }
}
