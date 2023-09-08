using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantEnterExitTrigger : MonoBehaviour
{
    private enum Type
    {
        Inside,
        Outside
    }

    [SerializeField] private Type type;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Customer customer) )
        {
            return;
        }

        switch (type)
        {
            case Type.Inside:
                HandleInside(customer);
                break;
            case Type.Outside:
                HandleOutside(customer);
                break;
        }
    }

    private void HandleInside(Customer customer)
    {
        if (customer.IsGoingToTable())
        {
            Door.Instance.CloseDoor();
        }
        else
        {
            Door.Instance.OpenDoor();
        }
    }
    
    private void HandleOutside(Customer customer)
    {
        if (customer.IsGoingToTable())
        {
            Door.Instance.OpenDoor();
        }
        else
        {
            Door.Instance.CloseDoor();
            customer.DestroySelf();
        }
    }
}
