using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Define 8 significant directions (unit circle points)
        Vector2[] directions = new Vector2[]
        {
        new Vector2(1, 0),                         // Right
        new Vector2(Mathf.Sqrt(0.5f), Mathf.Sqrt(0.5f)),   // Top-right
        new Vector2(0, 1),                         // Up
        new Vector2(-Mathf.Sqrt(0.5f), Mathf.Sqrt(0.5f)),  // Top-left
        new Vector2(-1, 0),                        // Left
        new Vector2(-Mathf.Sqrt(0.5f), -Mathf.Sqrt(0.5f)), // Bottom-left
        new Vector2(0, -1),                        // Down
        new Vector2(Mathf.Sqrt(0.5f), -Mathf.Sqrt(0.5f))   // Bottom-right
        };

        // Pick a random direction
        Vector2 spawnDir = directions[Random.Range(0, directions.Length)];
        Vector3 spawnPos = (Vector3)spawnDir * spawnRadius;

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().SetDirection(-spawnDir); // Move toward center
    }

}
