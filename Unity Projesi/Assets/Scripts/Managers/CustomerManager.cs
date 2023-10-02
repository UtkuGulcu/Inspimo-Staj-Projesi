using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance { get; private set; }

    public event EventHandler OnCustomerSpawned;

    [SerializeField] private GameObject[] customerPrefabArray;
    [SerializeField] private Transform restaurantEntryPoint;

    private Table[] tables;
    private float timer;
    private float timerMax = 40f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are more than one Customer Managers!!");
            Destroy(this);
        }
    }

    private void Start()
    {
        tables = FindObjectsByType<Table>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        TryToSpawnCustomer();
        DifficultyManager.Instance.OnDifficultyChanged += DifficultyManager_OnDifficultyChanged;
    }

    private void OnDisable()
    {
        DifficultyManager.Instance.OnDifficultyChanged -= DifficultyManager_OnDifficultyChanged;
    }

    private void DifficultyManager_OnDifficultyChanged(object sender, DifficultyManager.OnDifficultyChangedEventArgs e)
    {
        timerMax = e.newDifficulty == DifficultyManager.Difficulty.Medium ? 35f : 30f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timerMax)
        {
            timer = 0;
            TryToSpawnCustomer();
        }
    }

    private void TryToSpawnCustomer()
    {
        bool canSpawn = false;
        List<Table> availableTableList = new List<Table>();

        foreach (Table table in tables)
        {
            if (!table.IsOccupied())
            {
                canSpawn = true;
                availableTableList.Add(table);
            }
        }

        if (!canSpawn)
        {
            return;
        }

        int randomCustomerIndex = Random.Range(0, customerPrefabArray.Length);
        int randomTableIndex = Random.Range(0, availableTableList.Count);
        
        Customer customer = Instantiate(customerPrefabArray[randomCustomerIndex], restaurantEntryPoint.position, Quaternion.identity).GetComponent<Customer>();

        Table targetTable = availableTableList[randomTableIndex];
        customer.SetTargetTable(targetTable);
        OnCustomerSpawned?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetRestaurantEntryPoint()
    {
        return restaurantEntryPoint.position;
    }
}
