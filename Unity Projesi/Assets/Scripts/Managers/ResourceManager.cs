using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceManager : MonoBehaviour
{
    [Serializable]
    private struct ResourceKitchenObject
    {
        [SerializeField] public KitchenObjectSO kitchenObjectSO;
        [SerializeField] public ResourceType resourceType;
    }
    
    public enum ResourceType
    {
        Money,
        Meat,
        Bread,
        Tomato,
        Cabbage,
        CheeseBlock
    }

    public class OnMoneyChangedEventArgs : EventArgs
    {
        public int newMoneyAmount;
    }

    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnKitchenObjectResourceChanged;
    public event EventHandler<OnMoneyChangedEventArgs> OnMoneyChanged;

    [SerializeField] private List<ResourceKitchenObject> validResourceKitchenObjectList;
    
    private Dictionary<ResourceType, int> resourceDictionary;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are more than one Resource Managers!!");
            Destroy(this);
        }
        
        resourceDictionary = new Dictionary<ResourceType, int>
        {
            [ResourceType.Money] = 100,
            [ResourceType.Meat] = 10,
            [ResourceType.Bread] = 10,
            [ResourceType.Tomato] = 10,
            [ResourceType.Cabbage] = 10,
            [ResourceType.CheeseBlock] = 10
        };
    }

    public void IncreaseMoney(int addedAmount)
    {
        int newAmount = resourceDictionary[ResourceType.Money]; 
        newAmount += addedAmount;
        resourceDictionary[ResourceType.Money] = newAmount;
        InvokeOnMoneyChangedEvent(newAmount);
    }
    
    public void DecreaseMoney(int subtractedAmount)
    {
        int newAmount = resourceDictionary[ResourceType.Money]; 
        newAmount -= subtractedAmount;
        resourceDictionary[ResourceType.Money] = newAmount;
        InvokeOnMoneyChangedEvent(newAmount);
    }

    public void IncreaseKitchenObjectAmount(KitchenObjectSO kitchenObjectSO, int addedAmount)
    {
        foreach (ResourceKitchenObject resourceKitchenObject in validResourceKitchenObjectList)
        {
            if (resourceKitchenObject.kitchenObjectSO == kitchenObjectSO)
            {
                int newAmount = resourceDictionary[resourceKitchenObject.resourceType];
                newAmount += addedAmount;
                resourceDictionary[resourceKitchenObject.resourceType] = newAmount;
                InvokeOnKitchenObjectResourceChangedEvent();
            }
        }
    }
    
    public void DecreaseKitchenObjectAmount(KitchenObjectSO kitchenObjectSO, int subtractedAmount)
    {
        foreach (ResourceKitchenObject resourceKitchenObject in validResourceKitchenObjectList)
        {
            if (resourceKitchenObject.kitchenObjectSO == kitchenObjectSO)
            {
                int newAmount = resourceDictionary[resourceKitchenObject.resourceType];
                newAmount -= subtractedAmount;
                resourceDictionary[resourceKitchenObject.resourceType] = newAmount;
                InvokeOnKitchenObjectResourceChangedEvent();
            }
        }
    }

    public bool IsKitchenObjectAvailable(KitchenObjectSO kitchenObjectSO)
    {
        foreach (ResourceKitchenObject resourceKitchenObject in validResourceKitchenObjectList)
        {
            if (resourceKitchenObject.kitchenObjectSO == kitchenObjectSO)
            {
                return resourceDictionary[resourceKitchenObject.resourceType] > 0;
            }
        }

        return false;
    }

    public int GetResourceAmount(KitchenObjectSO kitchenObjectSO)
    {
        foreach (ResourceKitchenObject resourceKitchenObject in validResourceKitchenObjectList)
        {
            if (resourceKitchenObject.kitchenObjectSO == kitchenObjectSO)
            {
                return resourceDictionary[resourceKitchenObject.resourceType];
            }
        }

        Debug.LogError("Resource couldn't be found");
        return 0;
    }

    public int GetResourceAmount(ResourceType resourceType)
    {
        return resourceDictionary[resourceType];
    }
    
    public void SetResourceAmount(ResourceType resourceType, int newAmount)
    {
        resourceDictionary[resourceType] = newAmount;
    }

    private void InvokeOnKitchenObjectResourceChangedEvent()
    {
        OnKitchenObjectResourceChanged?.Invoke(this, EventArgs.Empty);
    }
    
    private void InvokeOnMoneyChangedEvent(int newAmount)
    {
        OnMoneyChanged?.Invoke(this, new OnMoneyChangedEventArgs
        {
            newMoneyAmount = newAmount
        });
    }
}
