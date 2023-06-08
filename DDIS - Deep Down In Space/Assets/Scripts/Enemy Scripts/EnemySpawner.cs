using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy to be spawned
    public Transform spawnPoint; // Empty game object representing the spawn point
    public float spawnRadius = 5f; // Radius around the spawn point where enemies can be spawned

    public int maxEnemies = 10; // Maximum number of enemies to be spawned
    private int currentEnemies = 0; // Current number of spawned enemies

    private void Update()
    {
        if (currentEnemies < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomOffset = Random.insideUnitCircle * spawnRadius; // Random offset within the spawn radius

        // Calculate the spawn position by adding the random offset to the spawn point's position
        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomOffset.x, 0f, randomOffset.y);

        // Instantiate the enemy prefab at the spawn position with no rotation
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Increase the enemy count
        currentEnemies++;
    }
}
