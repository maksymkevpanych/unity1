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
    
    private bool isSprinting = false;
    private float sprintTimer = 0f;
    private float sprintCooldownTimer = 0f;
    public float maxSprintDuration = 3f;
    public float sprintCooldown = 2f;

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
        

        // Обробка спринту
        if (Input.GetKey(KeyCode.LeftShift) && sprintCooldownTimer <= 0 && sprintTimer < maxSprintDuration)
        {
            isSprinting = true;
            sprintTimer += Time.deltaTime;
        }
        else
        {
            isSprinting = false;
        }

        if (isSprinting)
        {
            currentSpeed = Mathf.Min(currentSpeed + accelerationRate * Time.deltaTime, maxSpeed);
        }
        else
        {
            currentSpeed = baseSpeed;
            if (sprintTimer >= maxSprintDuration)
            {
                sprintCooldownTimer = sprintCooldown;
                sprintTimer = 0;
            }
        }

        if (sprintCooldownTimer > 0)
        {
            sprintCooldownTimer -= Time.deltaTime;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);
        console.log($'')
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
 
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}