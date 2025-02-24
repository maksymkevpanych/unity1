using UnityEngine;

public class SpiralMovement : MonoBehaviour
{
    public float a = 1f;  // Коефіцієнт, що визначає ріст спіралі
    public float speed = 2f;  // Швидкість руху
    private float t = 0f; // Часовий параметр
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        t += speed * Time.deltaTime;

        float r = a * t; // Радіус росте з часом
        float x = r * Mathf.Cos(t) + startPosition.x;
        float z = r * Mathf.Sin(t) + startPosition.z;

        transform.position = new Vector3(x, z, startPosition.z);
    }
}
