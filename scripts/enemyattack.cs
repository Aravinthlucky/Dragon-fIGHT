using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public PlayerController playerController; // Reference to the PlayerController
    private Transform targetPlayer; // Reference to the currently active player avatar
    public float attackRange = 5f; // Distance within which the enemy can attack
    public float attackInterval = 2f; // Time between attacks
    public bool fireToLeft = true; // Set to true if the projectile should fire to the left, false to fire right
    public float projectileSpeed = 5f; // Speed of the projectile

    private bool isAttacking; // Flag to prevent multiple projectiles firing

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Get reference to the PlayerController
        InvokeRepeating("FireProjectileInHorizontalLine", attackInterval, attackInterval); // Start firing projectiles in a horizontal direction
    }

    private void Update()
    {
        // Update the targetPlayer to the current active avatar
        targetPlayer = playerController.GetCurrentAvatar()?.transform;
    }

    // Method that checks whether the player is within range and attacks if true
    private void TryAttackPlayer()
    {
        if (targetPlayer != null && Vector2.Distance(transform.position, targetPlayer.position) <= attackRange && !isAttacking)
        {
            isAttacking = true; // Set attacking flag
            AttackPlayer(); // Perform the attack if within range
            Invoke("ResetAttackFlag", attackInterval); // Reset the attacking flag after the attack interval
        }
    }

    // Method to attack the player by calculating vertical or horizontal direction based on distance
    private void AttackPlayer()
    {
        if (targetPlayer != null)
        {
            // Instantiate the projectile at the enemy's position
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Calculate the distances to determine the direction to fire
                float verticalDistance = targetPlayer.position.y - transform.position.y;
                float horizontalDistance = targetPlayer.position.x - transform.position.x;

                Vector2 direction;

                // Fire vertically if the player is closer vertically
                if (Mathf.Abs(verticalDistance) > Mathf.Abs(horizontalDistance))
                {
                    direction = new Vector2(0, Mathf.Sign(verticalDistance)); // Move straight up or down
                    Debug.Log("Enemy fired a projectile towards the player vertically!");
                }
                else
                {
                    direction = new Vector2(Mathf.Sign(horizontalDistance), 0); // Move straight left or right
                    Debug.Log("Enemy fired a projectile towards the player horizontally!");
                }

                // Set the speed of the projectile
                rb.velocity = direction * projectileSpeed;

                // Optionally, set the projectile's rotation to face the direction of movement
                projectile.transform.up = direction; // Set projectile's rotation to the movement direction
            }
        }
    }

    // Method to fire a projectile in a straight horizontal line without considering the player's position
    private void FireProjectileInHorizontalLine()
    {
        // Instantiate the projectile at the enemy's position
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Determine the horizontal direction (left or right)
            Vector2 direction = fireToLeft ? Vector2.left : Vector2.right;

            // Set the projectile's velocity to move in the horizontal direction
            rb.velocity = direction * projectileSpeed;

            // Adjust the rotation of the projectile to face left or right
            if (fireToLeft)
            {
                projectile.transform.rotation = Quaternion.Euler(0, 0, 180); // Face left
            }
            else
            {
                projectile.transform.rotation = Quaternion.Euler(0, 0, 0); // Face right (default)
            }

            Debug.Log("Enemy fired a projectile in a straight horizontal line!");
        }
    }

    // Method to reset the attacking flag
    private void ResetAttackFlag()
    {
        isAttacking = false; // Reset the flag after the attack
    }
}
