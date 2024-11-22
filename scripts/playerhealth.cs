using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;                 // Maximum number of hearts (health)
    private int currentHealth;                 // Current health value

    public Image[] hearts;                     // Array to hold heart UI images
    public AudioClip healthReductionSound;     // Sound to play on health reduction
    private AudioSource audioSource;           // AudioSource component

    private void Start()
    {
        currentHealth = maxHealth;             // Initialize current health
        UpdateHearts();                        // Update the UI hearts on start
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return; // Exit if the player is already dead

        currentHealth -= damage;               // Decrease health
        Debug.Log("Player Health: " + currentHealth);
        PlayHealthReductionSound();             // Play sound on health reduction
        UpdateHearts();                        // Update UI after taking damage

        if (currentHealth <= 0)
        {
            currentHealth = 0;                  // Ensure health doesn't go below 0
            Die();                              // Call the die method when health is 0 or below
        }
    }

    private void UpdateHearts()
    {
        // Update heart visibility based on current health
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth; // Enable/Disable heart images
        }
    }

    private void Die()
    {
        // Notify GameManager that the player has died
        GameManager.Instance.OnPlayerDeath();    // Show game over panel
        Debug.Log("Player has died!");

        // Optionally disable the player or play a death animation
        gameObject.SetActive(false);              // Example of disabling the player
    }

    // Method to reset health to full when replaying the game
    public void ResetHealth()
    {
        currentHealth = maxHealth;               // Reset health to maximum
        UpdateHearts();                          // Update the hearts UI
        gameObject.SetActive(true);              // Re-enable the player if disabled
    }

    // Method to play the health reduction sound
    private void PlayHealthReductionSound()
    {
        if (healthReductionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(healthReductionSound); // Play the health reduction sound
        }
        else
        {
            Debug.LogWarning("Health reduction sound or AudioSource is not assigned!"); // Log a warning if the sound is not assigned
        }
    }
}
