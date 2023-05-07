using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // public float speed;
    // public float rotationSpeed;
    //
    // private Rigidbody rb;
    //
    // private void Awake()     
    // {
    //     rb = GetComponent<Rigidbody>();
    // }
    //
    // void Update()
    // {
    //     float horizontalInput = Input.GetAxis("Horizontal");
    //
    //     float verticalInput = Input.GetAxis("Vertical"); 
    //
    //     Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
    //     movementDirection.Normalize();      
    //
    //     rb.velocity = new Vector3(horizontalInput*speed, rb.velocity.y, verticalInput*speed);         
    //
    //     if(movementDirection != Vector3.zero) 
    //     {
    //         Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up); 
    //
    //         transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    //     }
    // }

    // public CharacterController controller;
    //
    // public float speed = 6f;
    //
    // public float turnSmoothTime = 0.1f;
    //
    // public float turnSmoothVelocity;
    // private void Update()
    // {
    //     float horizontal = Input.GetAxisRaw("Horizontal");
    //     float vertical = Input.GetAxisRaw("Vertical");
    //     
    //     Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
    //
    //     if (direction.magnitude >= 0.1f)
    //     {
    //
    //         float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    //         float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
    //             turnSmoothTime);
    //         transform.rotation = Quaternion.Euler(0f,angle,0f);
    //         controller.Move(direction * speed * Time.deltaTime);
    //     }
    // }
}