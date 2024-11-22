using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip jumpSound;  // Assign this in the Inspector
    public AudioClip fightSound; // Assign this in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component from the player GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Play jump sound when the player jumps
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Play fight sound when the player fights (replace "Fire1" with your action key)
        if (Input.GetButtonDown("Fire1"))
        {
            Fight();
        }
    }

    void Jump()
    {
        // Play the jump sound effect
        audioSource.PlayOneShot(jumpSound);
        
        // Jump logic here...
    }

    void Fight()
    {
        // Play the fight sound effect
        audioSource.PlayOneShot(fightSound);
        
        // Fighting logic here...
    }
}
