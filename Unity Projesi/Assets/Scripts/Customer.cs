using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    //public static event EventHandler OnAnyOrderPaid;

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
            transform.eulerAngles = new Vector3(0, 90, 0);
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
        //OnAnyOrderPaid?.Invoke(this, EventArgs.Empty);
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
