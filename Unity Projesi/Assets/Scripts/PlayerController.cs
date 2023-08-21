using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
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
        horizontalInput = FixedJoystick.Instance.Horizontal;
        verticalInput = FixedJoystick.Instance.Vertical;
        movementVector = new Vector3(horizontalInput, 0, verticalInput);
        movementVector.Normalize();
        
        if (verticalInput != 0 && horizontalInput != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        
    }

    private void FixedUpdate()
    {
        rb.velocity = movementVector * (movementSpeed * Time.deltaTime);
    }
}
