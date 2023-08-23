using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] visuals;
    
    private StoveCounter stoveCounter;

    private void Awake()
    {
        stoveCounter = GetComponent<StoveCounter>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if (e.state == StoveCounter.State.Idle)
        {
            DisableVisuals();
        }
        else
        {
            EnableVisuals();
        }
    }

    private void DisableVisuals()
    {
        foreach (GameObject visual in visuals)
        {
            visual.SetActive(false);
        }
    }
    
    private void EnableVisuals()
    {
        foreach (GameObject visual in visuals)
        {
            visual.SetActive(true);
        }
    }
    
}
