using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : Counter
{
    public static event EventHandler OnAnyObjectTrashed;
    
    public override void Interact(Player interactedPlayer)
    {
        if (interactedPlayer.HasKitchenObject())
        {
            interactedPlayer.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
