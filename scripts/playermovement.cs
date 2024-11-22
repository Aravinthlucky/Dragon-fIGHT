using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using UnityEngine.EventSystems; // Required for UI button press detection

public class PlayerController : MonoBehaviour
{
    // Movement and jump variables
    public float moveSpeed = 5f;        // Speed of the player movement
    public float jumpForce = 4f;        // Force of the jump
    private Rigidbody2D rb;             // Reference to the Rigidbody2D component
    private bool isGrounded;            // Check if the player is grounded
    public LayerMask groundLayer;       // Layer to check for ground
    private bool isJumping = false;     // Check if the player should jump
    private bool canMove = true;        // Can the player move?

    // Health variables
    public int health = 3;              // Player health

    // Weapon variables
    public Sprite normalAvatarSprite;   // Default sprite without a weapon
    public Sprite weapon1Sprite;        // Sprite for weapon 1
    public Sprite weapon2Sprite;        // Sprite for weapon 2
    private SpriteRenderer spriteRenderer; // Reference to the player's sprite renderer
    private bool hasWeapon = false;     // Whether the player has picked up a weapon
    private int currentWeapon = 0;      // Currently equipped weapon (0 = no weapon, 1 = weapon 1, etc.)
    private int weaponsCollected = 0;   // Count of weapons picked up

    // Avatar switching
    public GameObject normalAvatar;     // Default avatar GameObject
    private GameObject currentAvatar;   // Reference to the currently active avatar

    // Sound variables
    public AudioClip weaponPickupSound; // Sound for weapon pickup
    public AudioClip moveSound;         // Sound for left and right movement
    public AudioClip jumpSound;         // Sound for jumping
    private AudioSource audioSource;    // Reference to AudioSource

    // Reference to the fight button UI element
    public GameObject fightButton;

    // UI button movement tracking
    private bool moveLeft = false;
    private bool moveRight = false;

    void Start()
    {
        // Initialize Rigidbody2D, SpriteRenderer, and AudioSource
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>(); // Initialize AudioSource

        // Initialize the avatar as normal at the start
        spriteRenderer.sprite = normalAvatarSprite;
        ChangeAvatar(normalAvatar); // Initialize GameObject avatar

        // Ensure the fight button is hidden initially
        fightButton.SetActive(false);
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);

        // Apply movement and check for input to pick up a weapon
        if (canMove)
        {
            HandleInput(); // Handle input for weapon pickup and attacking
        }

        // Keep the player within camera bounds
        ClampPosition();

        // Handle UI button movement
        if (moveLeft)
        {
            MoveLeft(); // Move left if the left button is pressed
        }
        else if (moveRight)
        {
            MoveRight(); // Move right if the right button is pressed
        }
    }

    void HandleInput()
    {
        // Input for weapon pickup
        if (Input.GetKeyDown(KeyCode.E))
        {
            EquipWeapon(); // Equip weapon
        }

        // Input for attacking
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack(); // Call attack method
        }

        // Input for jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(); // Call jump function
        }
    }

    // Function to move the player left
    public void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        PlayMovementSound(); // Play sound for movement
    }

    // Function to move the player right
    public void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        PlayMovementSound(); // Play sound for movement
    }

    // Function to make the player jump
    public void Jump()
    {
        if (isGrounded) // Ensure the player can only jump if grounded
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            PlayJumpSound(); // Play jump sound
        }
    }

    // Function to stop the player's movement
    public void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y); // Stop horizontal movement
    }

    // Play sound for movement (left or right)
    private void PlayMovementSound()
    {
        if (moveSound != null && !audioSource.isPlaying) // Play the sound only if it's not already playing
        {
            audioSource.PlayOneShot(moveSound);
        }
    }

    // Play sound for jumping
    private void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    // Equip a weapon and change the sprite and avatar
    public void EquipWeapon()
    {
        if (!hasWeapon)
        {
            spriteRenderer.sprite = weapon1Sprite;  // Change sprite to weapon 1
            ChangeAvatar(normalAvatar);              // Change GameObject avatar
            currentWeapon = 1;
            hasWeapon = true;
            PlayWeaponPickupSound();                 // Play pickup sound for weapon 1
            weaponsCollected++;                      // Increment the weapon count
            Debug.Log("Equipped weapon 1 with new sprite!");
        }
        else if (currentWeapon == 1)
        {
            spriteRenderer.sprite = weapon2Sprite;  // Change sprite to weapon 2
            currentWeapon = 2;
            PlayWeaponPickupSound();                 // Play pickup sound for weapon 2
            weaponsCollected++;                      // Increment the weapon count
            Debug.Log("Equipped weapon 2 with new sprite!");
        }

        // Check if both weapons are collected
        ToggleFightButton();                       // Call method to toggle fight button visibility
    }

    // New method to show/hide the fight button based on weapon count
    private void ToggleFightButton()
    {
        if (weaponsCollected >= 2)
        {
            fightButton.SetActive(true);           // Show the fight button
        }
        else
        {
            fightButton.SetActive(false);          // Hide the fight button
        }
    }

    // Attack method
    public void Attack()
    {
        Debug.Log($"{currentAvatar.name} is attacking!"); // Example attack log
    }

    // Change the player's avatar GameObject
    public void ChangeAvatar(GameObject newAvatar)
    {
        if (currentAvatar != null)
        {
            currentAvatar.SetActive(false); // Deactivate the current avatar
        }

        currentAvatar = newAvatar; // Set the new avatar as the current one
        currentAvatar.SetActive(true); // Activate the selected avatar
    }

    public GameObject GetCurrentAvatar()
    {
        return currentAvatar; // Return the currently active avatar
    }

    // Play weapon pickup sound
    private void PlayWeaponPickupSound()
    {
        if (weaponPickupSound != null)
        {
            audioSource.PlayOneShot(weaponPickupSound);
        }
    }

    // Clamp the player's position within the camera's viewport
    void ClampPosition()
    {
        Vector3 position = transform.position;
        Camera camera = Camera.main;
        if (camera != null)
        {
            float halfHeight = camera.orthographicSize;
            float halfWidth = halfHeight * camera.aspect;
            float minX = camera.transform.position.x - halfWidth;
            float maxX = camera.transform.position.x + halfWidth;
            float minY = camera.transform.position.y - halfHeight;
            float maxY = camera.transform.position.y + halfHeight;

            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);

            transform.position = position; // Update player position
        }
    }

    // Methods for UI button press events
    public void OnMoveLeftPressed()
    {
        moveLeft = true;
    }

    public void OnMoveLeftReleased()
    {
        moveLeft = false;
        StopMoving();
    }

    public void OnMoveRightPressed()
    {
        moveRight = true;
    }

    public void OnMoveRightReleased()
    {
        moveRight = false;
        StopMoving();
    }
}
