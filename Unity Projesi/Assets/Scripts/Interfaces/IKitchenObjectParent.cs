using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectLocationTransform();
    public KitchenObject GetKitchenObject();
    public void SetKitchenObject(KitchenObject KitchenObject);
    public void ClearKitchenObject();
    public bool HasKitchenObject();
}
