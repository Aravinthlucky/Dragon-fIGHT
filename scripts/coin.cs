using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // The value of the coin

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object the coin collided with is tagged as the Player
        if (other.CompareTag("Player"))
        {
            // Call the UpdateScore method in GameManager to add the coin's value to the score
            GameManager.Instance.UpdateScore(coinValue);

            // Destroy the coin after it's collected
            Destroy(gameObject);
        }
    }
}
