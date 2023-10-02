using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private UpgradeItemUI stoveUpgrade;
    [SerializeField] private UpgradeItemUI movementSpeedUpgrade;
    [SerializeField] private FryingRecipeSO fryingRecipeSO;
    
    private class SaveObject
    {
        public int money;
        public int meatAmount;
        public int breadAmount;
        public int tomatoAmount;
        public int cabbageAmount;
        public int cheeseBlockAmount;
        public int stoveUpgradeCount;
        public int movementSpeedUpgradeCount;
        public float movementSpeed;
        public float stoveFryingTime;
    }
    
    public static SaveManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are multiple SaveManagers!!");
            Destroy(this);
        }
        
        SaveSystem.Initialize();
    }

    public void Save()
    {
        ResourceManager resourceManager = ResourceManager.Instance;
        PlayerController playerController = FindAnyObjectByType<PlayerController>(FindObjectsInactive.Exclude);

        SaveObject saveObject = new SaveObject
        {
            money = resourceManager.GetResourceAmount(ResourceManager.ResourceType.Money),
            meatAmount = resourceManager.GetResourceAmount(ResourceManager.ResourceType.Meat),
            breadAmount = resourceManager.GetResourceAmount(ResourceManager.ResourceType.Bread),
            tomatoAmount = resourceManager.GetResourceAmount(ResourceManager.ResourceType.Tomato),
            cabbageAmount = resourceManager.GetResourceAmount(ResourceManager.ResourceType.Cabbage),
            cheeseBlockAmount = resourceManager.GetResourceAmount(ResourceManager.ResourceType.CheeseBlock),
            stoveUpgradeCount = stoveUpgrade.GetUpgradeCount(),
            movementSpeedUpgradeCount = movementSpeedUpgrade.GetUpgradeCount(),
            movementSpeed = playerController.GetMovementSpeed(),
            stoveFryingTime = fryingRecipeSO.fryingTimerMax
        };

        string jsonString = JsonUtility.ToJson(saveObject);
        
        SaveSystem.Save(jsonString);
    }

    public void Load()
    {
        string saveString = SaveSystem.Load();

        if (saveString == null)
        {
            return;
        }
        
        SaveObject loadedSaveObject =  JsonUtility.FromJson<SaveObject>(saveString);

        ResourceManager.Instance.SetResourceAmount(ResourceManager.ResourceType.Money, loadedSaveObject.money);
        ResourceManager.Instance.SetResourceAmount(ResourceManager.ResourceType.Meat, loadedSaveObject.meatAmount);
        ResourceManager.Instance.SetResourceAmount(ResourceManager.ResourceType.Bread, loadedSaveObject.breadAmount);
        ResourceManager.Instance.SetResourceAmount(ResourceManager.ResourceType.Tomato, loadedSaveObject.tomatoAmount);
        ResourceManager.Instance.SetResourceAmount(ResourceManager.ResourceType.Cabbage, loadedSaveObject.cabbageAmount);
        ResourceManager.Instance.SetResourceAmount(ResourceManager.ResourceType.CheeseBlock, loadedSaveObject.cheeseBlockAmount);
        
        stoveUpgrade.SetUpgradeCount(loadedSaveObject.stoveUpgradeCount);
        movementSpeedUpgrade.SetUpgradeCount(loadedSaveObject.movementSpeedUpgradeCount);
        
        PlayerController[] playerControllerArray = FindObjectsByType<PlayerController>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (PlayerController playerController in playerControllerArray)
        {
            playerController.SetMovementSpeed(loadedSaveObject.movementSpeed);
        }

        fryingRecipeSO.fryingTimerMax = loadedSaveObject.stoveFryingTime;
    }
}
