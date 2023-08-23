using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : Counter
{
    public override void Interact()
    {
        if (!HasKitchenObject())
        {
            if (Player.Instance.HasKitchenObject())
            {
                Player.Instance.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!Player.Instance.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(Player.Instance);
            }
            else if (Player.Instance.GetKitchenObject().TryGetComponent(out Plate playerPlate))
            {
                if (playerPlate.TryToAddToPlate(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestroySelf();
                }
            }
            else if (GetKitchenObject().TryGetComponent(out Plate counterPlate))
            {
                if (counterPlate.TryToAddToPlate(Player.Instance.GetKitchenObject().GetKitchenObjectSO()))
                {
                    Player.Instance.GetKitchenObject().DestroySelf();
                    GetKitchenObject().SetKitchenObjectParent(Player.Instance);
                }
            }
            else
            {
                KitchenObject oldKitchenObject = Player.Instance.GetKitchenObject();
                oldKitchenObject.SetParentNull();
                GetKitchenObject().SetKitchenObjectParent(Player.Instance);
                oldKitchenObject.SetKitchenObjectParent(this);
            }
        }
    }
}
