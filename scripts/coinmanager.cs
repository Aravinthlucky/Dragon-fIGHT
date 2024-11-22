using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance; // Singleton instance

    public GameObject coinPrefab; // Assign your Coin prefab in the inspector
    public Text coinText; // Assign your UI Text in the inspector
    public GameObject player; // Reference to the player (to check for death)

    private int coinsCollected = 0; // Track the number of coins collected
    private bool isPlayerAlive = true; // Track if the player is alive
    private Camera mainCamera;

    

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera reference
        UpdateCoinText(); // Update the UI text at the start
        SpawnCoin(); // Spawn the first coin
    }

    public void SpawnCoin()
    {
        // Check if the player is alive before spawning the next coin
        if (!isPlayerAlive)
        {
            Debug.Log("Player is not alive, cannot spawn coin.");
            return;
        }

        // Ensure the mainCamera is properly set
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Re-assign mainCamera if null
            if (mainCamera == null)
            {
                Debug.LogError("Main camera not found!");
                return;
            }
        }

        // Calculate the bounds of the camera's viewport
        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;

        // Generate a random spawn position within the camera's bounds
        float randomX = Random.Range(mainCamera.transform.position.x - halfWidth, mainCamera.transform.position.x + halfWidth);
        
        // Ensure the Y-coordinate stays within visible bounds (considering the height of the coin)
        float randomY = Random.Range(mainCamera.transform.position.y - halfHeight + 0.5f, mainCamera.transform.position.y + halfHeight - 0.5f);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        // Log the spawn position to confirm it's within bounds
        Debug.Log("Attempting to spawn coin at: " + spawnPosition);

        // Ensure coinPrefab is assigned
        if (coinPrefab == null)
        {
            Debug.LogError("Coin prefab not assigned!");
            return;
        }

        GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity); // Spawn new coin

        // Check if the coin is active in the scene
        if (newCoin.activeInHierarchy)
        {
            Debug.Log("Coin spawned successfully.");
        }
        else
        {
            Debug.LogWarning("Coin spawned but is not active.");
        }
    }

    public void CollectCoin()
    {
        if (!isPlayerAlive)
        {
            Debug.LogWarning("Player is not alive, cannot collect coin.");
            return;
        }

        coinsCollected++; // Increment the collected coins count
        UpdateCoinText(); // Update the UI text

        // Spawn a new coin after collection, if the player is still alive
        SpawnCoin(); // Automatically spawn the next coin
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinsCollected; // Update the text to show collected coins
        }
    }

    public void OnPlayerDeath()
    {
        isPlayerAlive = false; // Set the player as dead
        Debug.Log("Player has died. Total coins collected: " + coinsCollected);
        // Optionally handle player death logic here (e.g., stop spawning coins)
    }

    public void RevivePlayer() // Optional revive method for testing
    {
        isPlayerAlive = true; // Revive the player
        SpawnCoin(); // Respawn a coin after revival
    }
}
