using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : Counter
{
    public override void Interact(Player interactedPlayer)
    {
        if (interactedPlayer.HasKitchenObject())
        {
            interactedPlayer.GetKitchenObject().DestroySelf();
        }
    }
}
