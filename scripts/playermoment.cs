using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public float jumpForce = 5f; // Force applied when jumping
    public Transform groundCheck; // Position to check if the player is on the ground
    public LayerMask groundLayer; // Layer of the ground
    private Rigidbody2D rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrows)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // Set the horizontal velocity
    }

    private void Jump()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump")) // Check for jump input
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Add an upward force
        }
    }
}

