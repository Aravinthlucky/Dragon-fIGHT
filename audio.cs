using UnityEngine;

public class AudioManagerController : MonoBehaviour
{
    public AudioClip backgroundMusic; // Assign your background music in the Inspector
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // Loop the music
        audioSource.volume = 1f; // Set volume to your desired level
    }

    public void PlayBackgroundMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
