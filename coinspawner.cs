using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Reference to the coin prefab
    public float spawnInterval = 2f; // Time interval between spawns
    public Transform[] spawnPoints; // Points where coins can spawn

    private void Start()
    {
        StartCoroutine(SpawnCoins()); // Start spawning coins
    }

    private IEnumerator SpawnCoins()
    {
        while (true) // Continuously spawn coins
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the spawn interval

            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate a new coin at the chosen spawn point
            Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
