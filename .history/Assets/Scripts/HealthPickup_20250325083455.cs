using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalStorage.Instance.CollectHealthPickup();
            GlobalStorage.Instance.lives = Mathf.Min(GlobalStorage.Instance.lives + healAmount, 100);
            Debug.Log("Health pickup collected! Current health: " + GlobalStorage.Instance.lives);
            Destroy(gameObject);
        }
    }
}
