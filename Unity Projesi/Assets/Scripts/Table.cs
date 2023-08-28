using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable, IKitchenObjectParent, IHasProgress
{
    private enum State
    {
        Idle,
        WaitingToOrder,
        WaitingOrder,
        Eating
    }
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    [SerializeField] private SelectedObjectVisual SelectedObjectVisual;
    [SerializeField] private Transform kitchenObjectLocationTransform;
    [SerializeField] private Transform chairSittingLocation;

    private KitchenObject kitchenObject;
    private bool isOccupied;
    
    public void Interact(Player interactedPlayer)
    {
        
    }
    
    public void StartInteracting()
    {
        SelectedObjectVisual.Show();
    }

    public void StopInteracting()
    {
        SelectedObjectVisual.Hide();
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
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public Vector3 GetChairSittingLocation()
    {
        return chairSittingLocation.position;
    }
}