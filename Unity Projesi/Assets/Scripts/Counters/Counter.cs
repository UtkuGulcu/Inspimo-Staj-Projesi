using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    [SerializeField] private SelectedObjectVisual SelectedObjectVisual;
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    public void StartInteracting()
    {
        SelectedObjectVisual.Show();
    }
    
    public void StopInteracting()
    {
        SelectedObjectVisual.Hide();
    }

    public virtual void Interact()
    {
        
    }
}
