using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{

    private CustomerVisual customerVisual;
    private NavMeshAgent navMeshAgent;
    private Vector3 chairSittingLocation;
    private Table targetTable;
    private bool isGoingToTable;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        customerVisual = GetComponent<CustomerVisual>();
        isGoingToTable = true;
    }

    private void Start()
    {
        customerVisual.StartMoving();
    }

    private void Update()
    {
        if (!navMeshAgent.enabled || !isGoingToTable)
        {
            return;
        }
        
        if (navMeshAgent.remainingDistance <= 0.1f)
        {
            customerVisual.StopMoving();
            transform.position = chairSittingLocation;
            navMeshAgent.enabled = false;
            targetTable.SetCustomer(this);
        }
    }
    
    public void SetTargetTable(Table targetTable)
    {
        this.targetTable = targetTable;
        navMeshAgent.SetDestination(targetTable.transform.position);
        chairSittingLocation = targetTable.GetChairSittingLocation();
        targetTable.SetOccupied();
    }

    public void LeaveRestaurant()
    {
        isGoingToTable = false;
        transform.position = targetTable.GetDropOffLocation();
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(CustomerManager.Instance.GetRestaurantEntryPoint());
        customerVisual.StartMoving();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public bool IsGoingToTable()
    {
        return isGoingToTable;
    }
}
