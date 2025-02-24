using UnityEngine;

public class RotateRod : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation

    void Update()
    {
        // Rotate the rod around the X-axis
        transform.Rotate(Vector3.up* rotationSpeed * Time.deltaTime);
    }
}
