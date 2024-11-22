using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health for the slider
    private float currentHealth; // Current health for the slider

    public Slider healthBar; // Reference to the UI Slider for health
    public int health = 3; // Total health, represented in hearts
    public Image[] hearts; // Array to hold the heart images
    public Sprite fullHeart; // Sprite for a full heart
    public Sprite emptyHeart; // Sprite for an empty heart

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health for slider
        UpdateHealthBar(); // Update health bar UI
        UpdateHearts(); // Initialize hearts to full
    }

    // Call this function whenever the player takes damage
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Decrease health by damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp health to maxHealth
        UpdateHealthBar(); // Update health bar UI

        // Calculate hearts based on remaining health
        health = Mathf.FloorToInt(currentHealth / (maxHealth / hearts.Length)); // Update heart count
        health = Mathf.Clamp(health, 0, hearts.Length); // Ensure health doesn't exceed the heart count
        UpdateHearts(); // Update hearts display

        if (currentHealth <= 0)
        {
            // Handle player death
            Debug.Log("Player has died!");
            // You can also call Game Over or restart logic here
        }
    }

    // Function to update the health bar UI
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth; // Update health bar value
        }
    }

    // Function to update the heart icons based on current health
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart; // Set full heart if health permits
            }
            else
            {
                hearts[i].sprite = emptyHeart; // Set empty heart if player has lost health
            }
        }
    }
}
