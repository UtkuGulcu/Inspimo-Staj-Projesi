using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceType
    {
        Money,
        Meat,
        Bread,
        Tomato,
        Cabbage,
        CheeseBlock
    }
    
    [Serializable]
    private struct ResourceKitchenObject
    {
        [SerializeField] public KitchenObjectSO kitchenObjectSO;
        [SerializeField] public ResourceType resourceType;
    }

    public static ResourceManager Instance { get; private set; }

    private const string MEAT_NAME = "Uncooked Meat";
    private const string BREAD_NAME = "Bread";
    private const string TOMATO_NAME = "Tomato";
    private const string CABBAGE_NAME = "Cabbage";
    private const string CHEESE_BLOCK_NAME = "Cheese Block";
    
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
    }

    private void Start()
    {
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

    public void IncreaseMoney(int amount)
    {
        resourceDictionary[ResourceType.Money] += amount;
    }
    
    public void DecreaseMoney(int amount)
    {
        resourceDictionary[ResourceType.Money] -= amount;
    }

    public void IncreaseKitchenObjectAmount(KitchenObjectSO kitchenObjectSO, int amount)
    {
        foreach (ResourceKitchenObject resourceKitchenObject in validResourceKitchenObjectList)
        {
            if (resourceKitchenObject.kitchenObjectSO == kitchenObjectSO)
            {
                resourceDictionary[resourceKitchenObject.resourceType]++;
            }
        }
    }
    
    public void DecreaseKitchenObjectAmount(KitchenObjectSO kitchenObjectSO, int amount)
    {
        foreach (ResourceKitchenObject resourceKitchenObject in validResourceKitchenObjectList)
        {
            if (resourceKitchenObject.kitchenObjectSO == kitchenObjectSO)
            {
                resourceDictionary[resourceKitchenObject.resourceType]--;
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
}
