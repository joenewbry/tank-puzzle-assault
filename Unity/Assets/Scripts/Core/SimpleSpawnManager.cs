using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawnManager : MonoBehaviour
{
    public List<Transform> SpawnPoints = new List<Transform>();
    public GameObject EnemyPrefab;
    public GameObject PlayerPrefab;

    private void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
    }

    private void SpawnPlayer()
    {
        if (PlayerPrefab != null && SpawnPoints.Count > 0)
        {
            Instantiate(PlayerPrefab, SpawnPoints[0].position, SpawnPoints[0].rotation);
        }
    }

    private void SpawnEnemies()
    {
        if (EnemyPrefab != null)
        {
            for (int i = 1; i < SpawnPoints.Count; i++)
            {
                Instantiate(EnemyPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            }
        }
    }

    public void AddSpawnPoint(Transform point)
    {
        SpawnPoints.Add(point);
    }
}