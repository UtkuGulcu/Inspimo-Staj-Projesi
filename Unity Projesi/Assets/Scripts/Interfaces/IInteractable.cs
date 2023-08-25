using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void StartInteracting();
    public void StopInteracting();
    public void Interact(Player interactedPlayer);
}
