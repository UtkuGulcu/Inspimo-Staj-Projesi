using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Customer customer) && !customer.IsGoingToTable())
        {
            customer.DestroySelf();
        }
    }
}
