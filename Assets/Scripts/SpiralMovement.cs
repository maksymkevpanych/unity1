using UnityEngine;

public class SpiralMovement : MonoBehaviour
{
    public float a = 1f;  
    public float speed = 2f;  
    private float t = 0f; 
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        t += speed * Time.deltaTime;

        float r = a * t; 
        float x = r * Mathf.Cos(t) + startPosition.x;
        float z = r * Mathf.Sin(t) + startPosition.z;

        transform.position = new Vector3(x, z, startPosition.z);
    }
}
