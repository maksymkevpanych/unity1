using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;     // Movement speed
    public float lookSpeed = 2f;     // Mouse sensitivity
    public float jumpForce = 5f;     // Jump force
    public float gravity = 9.81f;    // Gravity force

    private float rotationX = 0f;    // Rotation angle for looking up/down
    private float rotationY = 0f;    // Rotation angle for looking left/right
    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  // Lock cursor for FPS-style control
    }

    void Update()
    {
        // Get movement input
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Ensure proper ground detection
        if (controller.isGrounded)
        {
            velocity.y = -2f; // Small downward force to keep grounded properly

            // Jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpForce * 2 * gravity);
            }
        }

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Update rotation values
        rotationY += mouseX;          // Rotate left/right
        rotationX -= mouseY;          // Look up/down
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Prevent flipping

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, rotationY, 0); // Rotate body horizontally
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Rotate camera vertically
    }
}
