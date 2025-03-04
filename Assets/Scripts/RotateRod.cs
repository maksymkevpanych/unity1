using UnityEngine;

public class RotateRod : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        
        Vector3 pivotPoint = transform.position - transform.right * (transform.localScale.x / 2);

        
        transform.RotateAround(pivotPoint, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
