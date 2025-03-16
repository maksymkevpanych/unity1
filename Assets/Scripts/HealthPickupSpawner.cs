using UnityEngine;
using System.Collections;

public class HealthPickupSpawner : MonoBehaviour
{
    public GameObject healthPickupPrefab;  // Assign red cube prefab in Inspector
    public float spawnInterval = 10f;      // Time between spawns
    public float spawnRadius = 100f;       // Max spawn distance from player
    public float minSpawnDistance = 10f;   // Avoid spawning too close to player
    public int maxPickups = 5;             // Limit number of active pickups
    private int currentPickups = 0;
    public Transform player;               // Assign player in Inspector

    private void Start()
    {
        StartCoroutine(SpawnPickups());
    }

    private IEnumerator SpawnPickups()
    {
        while (true)
        {
            if (currentPickups < maxPickups)
            {
                SpawnHealthPickup();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnHealthPickup()
    {
        Vector3 spawnPosition = GetValidSpawnPosition();
        if (spawnPosition != Vector3.zero)
        {
            GameObject pickup = Instantiate(healthPickupPrefab, spawnPosition, Quaternion.identity);
            currentPickups++;
            StartCoroutine(DestroyPickupAfterTime(pickup, 30f)); // Destroy after 30s
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 10; i++) // Try 10 times to find a valid spot
        {
            Vector3 randomPos = player.position + (Random.insideUnitSphere * spawnRadius);
            randomPos.y = Terrain.activeTerrain.SampleHeight(randomPos) + 0.5f; // Adjust for terrain height

            if (Vector3.Distance(randomPos, player.position) >= minSpawnDistance)
            {
                return randomPos;
            }
        }
        return Vector3.zero; // No valid position found
    }

    private IEnumerator DestroyPickupAfterTime(GameObject pickup, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (pickup)
        {
            Destroy(pickup);
            currentPickups--;
        }
    }
}
