using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    
    [SerializeField] private SelectedObjectVisual SelectedObjectVisual;
    [SerializeField] private Transform kitchenObjectLocationTransform;

    private KitchenObject kitchenObject;

    public void StartInteracting()
    {
        SelectedObjectVisual.Show();
    }
    
    public void StopInteracting()
    {
        SelectedObjectVisual.Hide();
    }

    public virtual void Interact(Player interactedPlayer)
    {
        Debug.LogError("Base.Interact() should never be executed. All counters should implement their own!");
    }

    public Transform GetKitchenObjectLocationTransform()
    {
        return kitchenObjectLocationTransform;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
