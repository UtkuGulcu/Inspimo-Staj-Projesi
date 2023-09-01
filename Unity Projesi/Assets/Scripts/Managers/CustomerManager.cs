using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance { get; private set; }
    
    [SerializeField] private List<Table> tables;
    [SerializeField] private List<GameObject> customerPrefabList;
    [SerializeField] private Transform restaurantEntryPoint;

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
        TryToSpawnCustomer();
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

        int randomCustomerIndex = Random.Range(0, customerPrefabList.Count);
        int randomTableIndex = Random.Range(0, availableTableList.Count);
        Customer customer = Instantiate(customerPrefabList[randomCustomerIndex], restaurantEntryPoint.position, Quaternion.identity).GetComponent<Customer>();

        Table targetTable = availableTableList[randomTableIndex];
        customer.SetTargetTable(targetTable);
    }

    public Vector3 GetRestaurantEntryPoint()
    {
        return restaurantEntryPoint.position;
    }
}
