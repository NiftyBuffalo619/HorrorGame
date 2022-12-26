using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed;
    public Transform orientation;

    public float groundDrag;
    [Header("Ground Check")] public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public float horizontalInput;
    public float verticalInput;

    public Vector3 moveDirection;
    public Rigidbody rb;
    public Transform camera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //to make sure player doesn't wall over the map 
        rb.freezeRotation = true;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f , whatIsGround);
        InputFunction();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayerFunction();
    }

    private void InputFunction()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
    }

    private void MovePlayerFunction()
    {
        //calculate movement direction 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f, ForceMode.Force);
        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x , 0f , rb.velocity.z);
        //limit the velocity
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x , rb.velocity.y , limitedVel.z);
        }
    }

    private void AnimateCamera()
    {
        // from -0.1060 to -0.23
        int animphase = 0;
        switch (animphase)
        {
            case 1:
                
                break;
        }
    }
}