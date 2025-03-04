using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
 
    public float baseSpeed = 4f;  
    public float maxSpeed = 12f;  
    public float accelerationTime = 3f; 
    private float currentSpeed;
    private float accelerationRate;

    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
 
    Vector3 velocity;
    bool isGrounded;
 
    void Start()
    {
        currentSpeed = baseSpeed;
        accelerationRate = (maxSpeed - baseSpeed) / accelerationTime;
    }

    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
 
        Vector3 move = transform.right * x + transform.forward * z;

        
        if (move.magnitude > 0)
        {
            currentSpeed = Mathf.Min(currentSpeed + accelerationRate * Time.deltaTime, maxSpeed);
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);
 
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
 
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
