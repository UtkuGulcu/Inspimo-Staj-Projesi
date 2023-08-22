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
        }
    }
}
