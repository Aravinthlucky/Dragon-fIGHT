using System.Collections; // Required for using Coroutines
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;                   // Maximum health of the enemy
    public int currentHealth;                   // Current health of the enemy
    public Image[] hearts;                        // Array to hold heart UI images (representing health)
    public Sprite deathSprite;                    // Sprite to switch to upon death
    public GameObject deathEffectPrefab;          // Reference to a death effect (particle effect, explosion, etc.)
    
    private SpriteRenderer spriteRenderer;        // Reference to the enemy's sprite renderer
    private bool isDead = false;                  // Track if the enemy is dead to prevent multiple death triggers
    private GameManager gameManager;              // Reference to the GameManager

    // Property to check if the enemy is dead
    public bool IsDead => isDead;

    private void Start()
    {
        currentHealth = maxHealth;                // Initialize enemy's health
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer
        gameManager = FindObjectOfType<GameManager>();   // Find the GameManager instance
        UpdateHearts();                           // Update the UI hearts on start
    }

    // Function to reduce the enemy's health
    public void TakeDamage(int damage)
    {
        if (isDead)
            return; // If the enemy is already dead, exit the function

        currentHealth -= damage;                  // Reduce enemy's health by the damage value
        Debug.Log("Enemy Health: " + currentHealth);
        
        UpdateHearts();                           // Update UI hearts based on current health

        if (currentHealth <= 0)
        {
            currentHealth = 0;                     // Ensure health does not go below zero
            Die();                                 // Call the Die function if health is zero or less
        }
    }

    // Function to update the UI hearts based on current health
    public void UpdateHearts()
    {
        // Update heart visibility based on current health
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth; // Enable/Disable heart images
        }
    }

    // Function that handles the enemy's death
    private void Die()
    {
        if (isDead) return;                        // Prevent multiple death triggers
        isDead = true;                             // Mark the enemy as dead

        // Change to the death sprite
        if (deathSprite != null)
        {
            spriteRenderer.sprite = deathSprite;   // Switch to the death sprite
        }

        // Optional: Instantiate a death effect (e.g., particle effect or explosion)
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // Disable any other components (such as collider, movement, etc.) if needed
        GetComponent<Collider2D>().enabled = false; // Disable the collider so the enemy doesn't interact anymore

        // Notify the GameManager that the enemy is defeated
        if (gameManager != null)
        {
            gameManager.OnEnemyDefeated();         // Notify that the enemy is defeated
        }

        // Start the coroutine to destroy the enemy after a delay
        StartCoroutine(DestroyEnemyAfterDelay(2f)); // Delay in seconds
    }

    // Coroutine to destroy the enemy game object after a delay
    private IEnumerator DestroyEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);                        // Destroy the enemy game object after the delay
    }

    // Method to reset the enemy's health when replaying the game
    public void ResetHealth()
    {
        currentHealth = maxHealth;                 // Reset enemy health to maximum
        isDead = false;                            // Reset the death state
        UpdateHearts();                            // Update the hearts UI
        GetComponent<Collider2D>().enabled = true; // Re-enable the enemy's collider
    }
}
