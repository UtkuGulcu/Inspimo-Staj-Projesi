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
        }
    }

    public void SetDestination(Vector3 targetDestination)
    {
        navMeshAgent.SetDestination(targetDestination);
    }

    public void SetChairSittingLocation(Vector3 chairSittingLocation)
    {
        this.chairSittingLocation = chairSittingLocation;
    }
}
