using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject enemyPrefab;   
    public Transform[] spawnPoints;   
    [SerializeField] public float spawnInterval;  

    private float timer = 0f;
    private float counter = 0f;

    void Update()
    {
        // Simple timer using deltatime
        timer += Time.deltaTime;

        // When timer surpasses spawn interval, spawn an enemy
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // Reset timer
            counter += 1;
            if (counter >= 10 && spawnInterval!=0.5)
            {
                spawnInterval -= 0.5f;
                counter = 0;
            }
        }
    }

    void SpawnEnemy()
    {
        // Safety Check
        if (enemyPrefab == null || spawnPoints.Length < 2)
        {
            Debug.LogError("Need at least 2 Spawn Points to create a line!");
            return;
        }

        // Picks first spawnpoint
        int spawnA = Random.Range(0, spawnPoints.Length);
        
        // Picks next spawnpoint for a line
        int spawnB = (spawnA + 1) % spawnPoints.Length;

        // Gets position of corners
        Vector3 positionA = spawnPoints[spawnA].position;
        Vector3 positionB = spawnPoints[spawnB].position;

        // Picks percentage 0-1
        float randomPercent = Random.Range(0f, 1f);

        // Calculates spot between them.
        Vector3 spawnPos = Vector3.Lerp(positionA, positionB, randomPercent);

        // 6. Instantiate the enemy at this new calculated position
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}