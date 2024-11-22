using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform playerNormalAvatar;  // Reference to the normal player's Transform
    public Transform playerWeaponAvatar;  // Reference to the weapon-equipped player's Transform
    public float moveSpeed = 3f;          // Movement speed
    public float followDistance = 1f;     // Minimum distance to follow the player vertically
    public LayerMask playerLayer;         // LayerMask to identify player layers

    private Transform targetPlayer;        // Variable to track the currently targeted player avatar

    // Update is called once per frame
    private void Update()
    {
        // Determine the target player based on the active avatar
        if (playerWeaponAvatar.gameObject.activeSelf)
        {
            targetPlayer = playerWeaponAvatar;  // Follow the weapon avatar if active
        }
        else
        {
            targetPlayer = playerNormalAvatar;  // Follow the normal avatar if weapon avatar is inactive
        }

        // Move towards the target player
        if (targetPlayer != null)
        {
            // Move towards the target player's vertical position (Y axis)
            float targetY = targetPlayer.position.y;

            // If the enemy is outside the follow distance, move towards the target
            if (Mathf.Abs(transform.position.y - targetY) > followDistance)
            {
                // Check for collision with the player
                if (!IsCollidingWithPlayer())
                {
                    Vector2 targetPosition = new Vector2(transform.position.x, targetY); // Keep X position unchanged
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                }
            }
        }
    }

    // Check for collision with the player
    private bool IsCollidingWithPlayer()
    {
        // Check if the enemy is colliding with the player using a small circle around the enemy
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.1f, playerLayer);
        return hits.Length > 0;
    }

    // Method to follow the normal player avatar explicitly
    public void FollowNormalAvatar()
    {
        targetPlayer = playerNormalAvatar;
    }

    // Method to follow the weapon-equipped player avatar explicitly
    public void FollowWeaponAvatar()
    {
        targetPlayer = playerWeaponAvatar;
    }
}
