using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform interactionOrigin;
    [SerializeField] [Range(0, 3)] private float interactionDistance;
    [SerializeField] private LayerMask interactionLayer;
    private GameObject lastInteractedObject;

    private void Start()
    {
        UIManager.Instance.OnInteractButtonDown += UIManager_OnInteractButtonDown;
        UIManager.Instance.OnAlternateInteractButtonDown += UIManager_OnAlternateInteractButtonDown;
    }

    private void OnDisable()
    {
        UIManager.Instance.OnInteractButtonDown -= UIManager_OnInteractButtonDown;
        UIManager.Instance.OnAlternateInteractButtonDown -= UIManager_OnAlternateInteractButtonDown;
    }

    private void Update()
    {
        // If this ray hits an interactable object
        if (Physics.Raycast(interactionOrigin.position, transform.forward, out RaycastHit hit, interactionDistance))
        {
            // If this is the first frame of the hit
            if (hit.transform.TryGetComponent(out IInteractable InteractableOnRange) && lastInteractedObject == null)
            {
                lastInteractedObject = hit.transform.gameObject;
                InteractableOnRange.StartInteracting();
            }
        }
        // If interaction stops
        else if (lastInteractedObject != null)
        {
            lastInteractedObject.GetComponent<IInteractable>().StopInteracting();
            lastInteractedObject = null;
        }
    }
    
    private void UIManager_OnAlternateInteractButtonDown(object sender, EventArgs e)
    {
        if (Physics.Raycast(interactionOrigin.position, transform.forward, out RaycastHit hit, interactionDistance, interactionLayer))
        {
            if (hit.transform.TryGetComponent(out IAlternateInteractable AlternateInteractable))
            {
                AlternateInteractable.AlternateInteract();
            }
        }
    }

    private void UIManager_OnInteractButtonDown(object sender, EventArgs e)
    {
        if (Physics.Raycast(interactionOrigin.position, transform.forward, out RaycastHit hit, interactionDistance, interactionLayer))
        {
            if (hit.transform.TryGetComponent(out IInteractable Interactable))
            {
                Interactable.Interact();
            }
        }
    }
}
