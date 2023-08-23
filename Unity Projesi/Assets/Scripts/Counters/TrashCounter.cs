using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : Counter
{
    public override void Interact()
    {
        if (Player.Instance.HasKitchenObject())
        {
            Player.Instance.GetKitchenObject().DestroySelf();
        }
    }
}
