using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenObject
{
    public class OnObjectAddedEventArgs : EventArgs
    {
        public KitchenObjectSO addedObject;
    }
    
    public event EventHandler<OnObjectAddedEventArgs> OnObjectAdded;
    
    [SerializeField] private List<KitchenObjectSO> ValidObjects;

    private List<KitchenObjectSO> objectsInPlate;

    private void Start()
    {
        objectsInPlate = new List<KitchenObjectSO>();
    }

    public bool TryToAddToPlate(KitchenObjectSO kitchenObjectSO)
    {
        if (!ValidObjects.Contains(kitchenObjectSO))
        {
            return false;
        }

        if (objectsInPlate.Contains(kitchenObjectSO))
        {
            return false;
        }

        objectsInPlate.Add(kitchenObjectSO);
        OnObjectAdded?.Invoke(this, new OnObjectAddedEventArgs
        {
            addedObject = kitchenObjectSO
        });

        return true;
    }
}
