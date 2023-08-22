using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable, IKitchenObjectParent
{
    [SerializeField] private SelectedObjectVisual SelectedObjectVisual;
    [SerializeField] private Transform kitchenObjectLocationTransform;

    private KitchenObject kitchenObject;
    
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
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

    public virtual void Interact()
    {
        Debug.LogError("Base.Interact() shouldn't executed. All counters should implement their own methods");
    }

    public Transform GetKitchenObjectLocationTransform()
    {
        return kitchenObjectLocationTransform;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject KitchenObject)
    {
        this.kitchenObject = KitchenObject;
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
