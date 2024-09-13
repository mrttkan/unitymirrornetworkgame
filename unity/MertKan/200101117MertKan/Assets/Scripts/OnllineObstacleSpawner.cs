using UnityEngine;
using Mirror;

public class OnlineObstacleSpawner : NetworkBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float spawnPositionX = 10.5f;
    public float spawnPositionY = -3.5f;
    
    private float timer;

    void Start()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("Obstacle Prefab is not assigned in the OnlineObstacleSpawner script.");
        }
    }

    void Update()
    {
        if (!isServer) // Only run this on the server
        {
            return;
        }

        if (obstaclePrefab == null)
        {
            Debug.LogError("Obstacle Prefab is not assigned.");
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }
    }

    [Server]
    void SpawnObstacle()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("Obstacle Prefab is not assigned.");
            return;
        }

        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(obstacle);
    }
}
