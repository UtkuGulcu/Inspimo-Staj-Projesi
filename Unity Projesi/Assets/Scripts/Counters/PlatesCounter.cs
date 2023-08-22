using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : Counter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact()
    {
        if (!Player.Instance.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, Player.Instance);
        }
    }
}
