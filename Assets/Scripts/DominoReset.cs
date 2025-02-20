using UnityEngine;

public class DominoReset : MonoBehaviour
{
    public GameObject[] cubes; // Assign your cubes in the Inspector
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    void Start()
    {
        // Store initial positions and rotations
        initialPositions = new Vector3[cubes.Length];
        initialRotations = new Quaternion[cubes.Length];

        for (int i = 0; i < cubes.Length; i++)
        {
            initialPositions[i] = cubes[i].transform.position;
            initialRotations[i] = cubes[i].transform.rotation;
        }
    }

    void Update()
    {
        // Press "R" to reset cubes to their original positions
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCubes();
        }
    }

    public void ResetCubes()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].transform.position = initialPositions[i];
            cubes[i].transform.rotation = initialRotations[i];

            // Reset Rigidbody velocity so they don't keep falling
            Rigidbody rb = cubes[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
