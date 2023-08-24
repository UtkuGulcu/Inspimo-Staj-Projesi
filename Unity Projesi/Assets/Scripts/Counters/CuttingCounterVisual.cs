using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT_TRIGGER = "Cut";
    private static readonly int CutTriggerID = Animator.StringToHash(CUT_TRIGGER);
    
    private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        cuttingCounter = GetComponent<CuttingCounter>();
        animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
    }

    private void CuttingCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if (e.progressNormalized != 0)
        {
            animator.SetTrigger(CutTriggerID);
        }
    }
}
