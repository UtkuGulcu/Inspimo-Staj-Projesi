using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerController playerController;
    private float footstepTimer;
    private float footstepTimerMax = 0.1f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        footstepTimer += Time.deltaTime;

        if (footstepTimer < footstepTimerMax)
        {
            return;
        }
        
        footstepTimer = 0f;

        if (playerController.IsWalking())
        {
            SoundReferences.Instance.PlayFootstepsSound(transform.position);
        }
    }
}
