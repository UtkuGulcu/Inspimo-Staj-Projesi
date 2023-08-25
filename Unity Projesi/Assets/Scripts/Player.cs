using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    //public static Player Instance { get; private set; }

    [SerializeField] private Transform pickupPoint;

    private KitchenObject kitchenObject;
    private PlayerController playerController;
    private PlayerInteraction playerInteraction;
    private PlayerVisual playerVisual;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerVisual = GetComponent<PlayerVisual>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    public Transform GetKitchenObjectLocationTransform()
    {
        return pickupPoint;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void EnablePlayer()
    {
        playerController.enabled = true;
        playerVisual.enabled = true;
        playerInteraction.enabled = true;
        playerInteraction.SubscribeToInteractEvents();
    }
    
    public void DisablePlayer()
    {
        playerController.enabled = false;
        playerVisual.enabled = false;
        playerInteraction.enabled = false;
    }
}
