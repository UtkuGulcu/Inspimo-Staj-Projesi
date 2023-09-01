using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    
    public enum ResourceType
    {
        Money,
        Meat,
        Bread,
        Tomato,
        Cabbage,
        Cheese
    }

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
        resourceDictionary[ResourceType.Money] = 100;
        resourceDictionary[ResourceType.Meat] = 10;
        resourceDictionary[ResourceType.Bread] = 10;
        resourceDictionary[ResourceType.Tomato] = 10;
        resourceDictionary[ResourceType.Cabbage] = 10;
        
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
        switch (kitchenObjectSO.kitchenObjectName)
        {
            case "Meat":
                resourceDictionary[ResourceType.Meat] += amount;
                break;
            case "Bread":
                resourceDictionary[ResourceType.Bread] += amount;
                break;
            case "Tomato":
                resourceDictionary[ResourceType.Tomato] += amount;
                break;
            case "Cabbage":
                resourceDictionary[ResourceType.Cabbage] += amount;
                break;
            case "Cheese":
                resourceDictionary[ResourceType.Cheese] += amount;
                break;
        }
    }
    
    public void DecreaseKitchenObjectAmount(KitchenObjectSO kitchenObjectSO, int amount)
    {
        switch (kitchenObjectSO.kitchenObjectName)
        {
            case "Meat":
                resourceDictionary[ResourceType.Meat] -= amount;
                break;
            case "Bread":
                resourceDictionary[ResourceType.Bread] -= amount;
                break;
            case "Tomato":
                resourceDictionary[ResourceType.Tomato] -= amount;
                break;
            case "Cabbage":
                resourceDictionary[ResourceType.Cabbage] -= amount;
                break;
            case "Cheese":
                resourceDictionary[ResourceType.Cheese] -= amount;
                break;
        }
    }

    public bool IsKitchenObjectAvailable(KitchenObjectSO kitchenObjectSO)
    {
        return kitchenObjectSO.kitchenObjectName switch
        {
            "Meat" => resourceDictionary[ResourceType.Meat] > 0,
            "Bread" => resourceDictionary[ResourceType.Bread] > 0,
            "Tomato" => resourceDictionary[ResourceType.Tomato] > 0,
            "Cabbage" => resourceDictionary[ResourceType.Cabbage] > 0,
            "Cheese" => resourceDictionary[ResourceType.Cheese] > 0,
        };
    }
}
