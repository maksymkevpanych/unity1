using UnityEngine;
using UnityEngine.AI; // Required for NavMesh

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 10;
    public float minSpawnDistance = 50f; // Minimum distance from player
    public float maxSpawnDistance = 100f; // Maximum distance from player
    public Terrain terrain;
    public Transform player;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            if (spawnPosition != Vector3.zero) // Ensure valid position
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        int maxAttempts = 100;
        int attempts = 0;
        Vector3 spawnPosition = Vector3.zero;
        NavMeshHit hit;

        while (attempts < maxAttempts)
        {
            Vector3 randomPosition = GetRandomPositionNearPlayer();
            
            // Check if the position is between min and max distance from the player
            float distance = Vector3.Distance(randomPosition, player.position);
            if (distance >= minSpawnDistance && distance <= maxSpawnDistance)
            {
                // Sample a position on the NavMesh
                if (NavMesh.SamplePosition(randomPosition, out hit, 5f, NavMesh.AllAreas))
                {
                    spawnPosition = hit.position;
                    break;
                }
            }
            attempts++;
        }

        return spawnPosition;
    }

    private Vector3 GetRandomPositionNearPlayer()
    {
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        float randomAngle = Random.Range(0f, 360f); // Pick a random direction

        Vector3 direction = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));
        Vector3 spawnPoint = player.position + direction * randomDistance;

        // Get terrain height
        float y = terrain.SampleHeight(spawnPoint);
        return new Vector3(spawnPoint.x, y, spawnPoint.z);
    }
}
