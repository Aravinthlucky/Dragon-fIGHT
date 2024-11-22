using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Button attackButton;          // Reference to the attack button in the UI
    public Sprite normalSprite;          // Normal sprite for the player
    public Sprite fightingSprite;        // Fighting sprite for the player
    public Sprite idleSprite;            // Idle sprite or another state sprite for the player
    public Transform attackPoint;        // The point from where the attack is triggered (in front of the player)
    public float attackRange = 1.5f;     // Range of the attack
    public int attackDamage = 1;         // Damage dealt to the enemy
    public LayerMask enemyLayer;         // Layer of enemies to attack
    public GameObject crashEffectPrefab; // Reference to the crash effect prefab (optional)

    private SpriteRenderer spriteRenderer; // Sprite renderer to switch between sprites
    private bool isAttacking = false;     // Flag to check if the player is currently attacking
    private float attackDuration = 0.5f;  // Duration of the attack in seconds
    private float currentAttackDuration;
    private Sprite previousSprite;        // To store the previous sprite

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer component

        // Set up the button listener for attack (if using a UI button)
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(OnFightButtonPressed); // Assign the attack action to button press
        }
    }

    private void Update()
    {
        // Check if the player is attacking
        if (isAttacking)
        {
            currentAttackDuration -= Time.deltaTime;
            if (currentAttackDuration <= 0)
            {
                StopAttack(); // End the attack after the duration
            }
        }

        // Attack input using spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnFightButtonPressed(); // Perform attack on spacebar press
        }
    }

    public void OnFightButtonPressed()
    {
        if (!isAttacking)
        {
            StartAttack(); // Begin attack when button or spacebar is pressed
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        previousSprite = spriteRenderer.sprite; // Store the previous sprite
        spriteRenderer.sprite = fightingSprite; // Change to fighting sprite
        currentAttackDuration = attackDuration; // Set the duration of the attack
        Attack(); // Call the attack function to damage enemies
    }

    private void StopAttack()
    {
        isAttacking = false;
        spriteRenderer.sprite = previousSprite; // Return to the previous sprite
    }

    public void Attack()
    {
        // Detect enemies in the attack range using OverlapCircle
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Loop through each enemy detected
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy")) // Ensure that the object hit is tagged as "Enemy"
            {
                // Get the enemy health component
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // Apply damage to the enemy
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log("Hit enemy: " + enemy.name + " | Remaining Health: " + enemyHealth.currentHealth);

                    // Instantiate crash effect at the enemy's position
                    if (crashEffectPrefab != null)
                    {
                        GameObject crashEffect = Instantiate(crashEffectPrefab, enemy.transform.position, Quaternion.identity);
                        Destroy(crashEffect, 0.5f); // Destroy the crash effect after 0.5 seconds
                    }
                }
            }
        }
    }

    // Draw the attack range in the Scene view for visualization
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        // Draw a wire sphere in the Scene view representing the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
