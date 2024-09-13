using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (isSpawning)
        {
            Instantiate(obstaclePrefab, new Vector3(10.5f, -3.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
