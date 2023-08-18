using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(1, 20)] private float movementSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movementVector;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // horizontalInput = 0;
        // verticalInput  = 0;

        horizontalInput = FixedJoystick.Instance.Horizontal;
        verticalInput = FixedJoystick.Instance.Vertical;
        movementVector = new Vector3(horizontalInput, 0, verticalInput);
        movementVector.Normalize();
        


        // if (FixedJoystick.Instance.Horizontal >= 0.25f)
        // {
        //     horizontalInput = 1;
        // }
        //
        // if (FixedJoystick.Instance.Horizontal <= -0.25f)
        // {
        //     horizontalInput = -1;
        // }
        //
        // if (FixedJoystick.Instance.Vertical >= 0.25f)
        // {
        //     verticalInput = 1;
        // }
        //
        // if (FixedJoystick.Instance.Vertical <= -0.25f)
        // {
        //     verticalInput = -1;
        // }
        
        if (FixedJoystick.Instance.Vertical != 0 && FixedJoystick.Instance.Horizontal != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontalInput * movementSpeed, 0, verticalInput * movementSpeed);
    }
}
