using UnityEngine;

public class CycloidMovement : MonoBehaviour
{
    public float radius = 2f;
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

        float x = radius * (t - Mathf.Sin(t)) + startPosition.x;
        float y = radius * (1 - Mathf.Cos(t)) + startPosition.y;

        transform.position = new Vector3(x, y, startPosition.z);

        if (x >= startPosition.x + 15f)
        {
            t = 0f;
        }
    }
}
