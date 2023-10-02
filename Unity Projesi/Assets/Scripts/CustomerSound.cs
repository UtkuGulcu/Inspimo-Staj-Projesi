using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerSound : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private float footstepTimer;
    private float footstepTimerMax = 0.1f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!navMeshAgent.enabled)
        {
            return;
        }
        
        footstepTimer += Time.deltaTime;

        if (footstepTimer >= footstepTimerMax)
        {
            footstepTimer = 0f;
            float volume = 0.6f;
            SoundReferences.Instance.PlayFootstepsSound(transform.position, volume);
        }
        
    }
}
