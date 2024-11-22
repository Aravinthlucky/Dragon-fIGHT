using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject currentAvatar; // The current avatar
    public GameObject newAvatarPrefab; // The new avatar prefab to switch to
    private bool canPickup = false;

    void Update()
    {
        // Check for input to pick up the object
        if (canPickup && Input.GetKeyDown(KeyCode.F))
        {
            PickUpObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object the player is colliding with is pickable
        if (other.CompareTag("Collectible"))
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            canPickup = false;
        }
    }

    private void PickUpObject()
    {
        // Change the avatar
        if (newAvatarPrefab != null)
        {
            Instantiate(newAvatarPrefab, transform.position, transform.rotation);
            Destroy(currentAvatar); // Remove the current avatar
            currentAvatar = null; // Set to null as it has been destroyed
        }

        // Optionally destroy the collectible object
        GameObject collectible = GameObject.FindGameObjectWithTag("Collectible");
        if (collectible != null)
        {
            Destroy(collectible);
        }
        
        canPickup = false; // Reset pickup state
    }
}
