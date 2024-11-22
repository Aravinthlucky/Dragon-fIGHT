using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the enemy movement
    public Transform normalAvatar; // Reference to the normal player avatar
    public Transform weaponAvatar; // Reference to the weapon avatar

    private Transform currentTarget; // The current target the enemy is following
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Start by following the normal avatar
        currentTarget = normalAvatar;

        // Get the main camera
        mainCamera = Camera.main;

        // Calculate the screen bounds
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        // Get the width and height of the enemy object (including its sprite size)
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void Update()
    {
        MoveTowardsTarget();
        ClampPosition();
    }

    // Method to move towards the current target
    void MoveTowardsTarget()
    {
        if (currentTarget != null)
        {
            // Calculate the direction to the target
            Vector2 direction = (currentTarget.position - transform.position).normalized;

            // Only allow movement in the upward and rightward directions
            direction.x = Mathf.Max(0, direction.x); // No left movement
            direction.y = Mathf.Max(0, direction.y); // No downward movement

            // Move the enemy in the allowed direction
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    void ClampPosition()
    {
        // Get the current position
        Vector3 position = transform.position;

        // Clamp the position within the screen bounds, considering the enemy's width and height
        position.x = Mathf.Clamp(position.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        position.y = Mathf.Clamp(position.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        // Update the enemy's position after clamping
        transform.position = position;
    }

    // Method to switch the target to the weapon avatar
    public void SwitchToWeaponAvatar()
    {
        currentTarget = weaponAvatar;
    }
}
