using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 5f;      // Speed of the player movement
    public float jumpForce = 7f;      // Force of the jump
    public LayerMask groundLayer;      // Layer to check for ground
    private Rigidbody2D rb;            // Reference to the Rigidbody2D component
    private bool isGrounded;           // Check if the player is grounded
    private bool isJumping = false;    // Check if the player should jump
    private float moveDirection = 0f;  // Move direction

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Handle keyboard input
        HandleKeyboardInput();

        // Apply movement based on input
        ApplyMovement();
    }

    private void HandleKeyboardInput()
    {
        // Get raw horizontal input for immediate stop when key is released
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveDirection = moveHorizontal; // Directly set moveDirection to horizontal input

        // Handle jump input via keyboard
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true; // Set jumping to true when space is pressed
        }
    }

    // Button click methods for UI
    public void MoveLeft()
    {
        moveDirection = -1f; // Move left
    }

    public void MoveRight()
    {
        moveDirection = 1f; // Move right
    }

    public void StopMoving()
    {
        moveDirection = 0f; // Stop movement when releasing the button
    }

    public void Jump()
    {
        if (isGrounded)
        {
            isJumping = true; // Set jumping to true
        }
    }

    private void ApplyMovement()
    {
        // Apply movement
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Handle Jump if allowed
        if (isJumping && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false; // Reset jump flag after applying jump
        }
    }

    // Ground check method
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((groundLayer & (1 << collision.gameObject.layer)) != 0) // Check if collided with ground layer
        {
            isGrounded = true; // Set isGrounded to true when touching the ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((groundLayer & (1 << collision.gameObject.layer)) != 0) // Check if exited ground layer
        {
            isGrounded = false; // Set isGrounded to false when leaving the ground
        }
    }
}
