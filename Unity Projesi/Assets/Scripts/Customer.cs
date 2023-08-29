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

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        customerVisual = GetComponent<CustomerVisual>();
    }

    private void Start()
    {
        customerVisual.StartMoving();
    }

    private void Update()
    {
        if (!navMeshAgent.enabled)
        {
            return;
        }
        
        if (navMeshAgent.remainingDistance <= 0.1f)
        {
            customerVisual.StopMoving();
            transform.position = chairSittingLocation;
            navMeshAgent.enabled = false;
            targetTable.ChangeState(Table.State.WaitingToOrder);
        }
    }
    
    public void SetTargetTable(Table targetTable)
    {
        this.targetTable = targetTable;
        navMeshAgent.SetDestination(targetTable.transform.position);
        chairSittingLocation = targetTable.GetChairSittingLocation();
    }
}
