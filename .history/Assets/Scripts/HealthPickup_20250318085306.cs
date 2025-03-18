using UnityEngine;



public class HealthPickup : MonoBehaviour
{
    public int healAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name); // Debugging

        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player collided with health pickup!"); // Log successful collision

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healAmount);
                Debug.Log("Health Restored!"); 
                Destroy(gameObject); // Destroy pickup
            }
            else
            {
                Debug.LogWarning("PlayerHealth script not found on Player!");
            }
        }
    }
}

