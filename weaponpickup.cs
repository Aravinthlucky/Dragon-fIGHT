using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Sprite weaponSprite; // Assign the weapon sprite in the Inspector
    private PlayerController playerController; // Reference to the player controller
    public float pickupRange = 1.5f; // Set the range within which the player can pick up the weapon

    private void Start()
    {
        // Find the player controller (assumes the player has the PlayerController component)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void Update()
    {
        // Check if the player is within pickup range
        if (playerController != null && IsPlayerInRange())
        {
            // Detect touch on the screen
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                // Check if the touch began
                if (touch.phase == TouchPhase.Began)
                {
                    // Convert touch position to world point
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                    // Check if the weapon sprite is touched using Raycast
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        // Call the EquipWeapon method if the player touches the weapon
                        EquipWeapon();
                    }
                }
            }
        }
    }

    private bool IsPlayerInRange()
    {
        // Calculate the distance between the player and the weapon
        float distance = Vector2.Distance(playerController.transform.position, transform.position);
        return distance <= pickupRange; // Return true if the distance is within the pickup range
    }

    private void EquipWeapon()
    {
        // Equip the weapon
        if (playerController != null)
        {
            playerController.EquipWeapon(); // Call the EquipWeapon method from PlayerController

            // Change the player's sprite to the weapon's sprite
            if (weaponSprite != null)
            {
                playerController.GetComponent<SpriteRenderer>().sprite = weaponSprite;
            }

            // Destroy the weapon pickup object after picking up
            Destroy(gameObject);
        }
    }
}
