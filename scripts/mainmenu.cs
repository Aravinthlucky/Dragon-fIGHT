using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI; // For UI elements
using System.Collections; // Required for IEnumerator

public class MenuController : MonoBehaviour
{
    public AudioManagerController audioManager; // Reference to the AudioManagerController script

    void Start()
    {
        // Ensure the audio manager is playing background music
        if (audioManager != null)
        {
            audioManager.PlayBackgroundMusic();
        }
        else
        {
            Debug.LogWarning("AudioManagerController reference is missing.");
        }
    }

    // Method to start the game
    public void StartGame()
    {
        // Start the coroutine to load the game scene
        StartCoroutine(LoadGameScene());
    }

    // Coroutine to load the game scene with an optional delay
    private IEnumerator LoadGameScene()
    {
        // Optionally, show a loading screen or animation here
        yield return new WaitForSeconds(1f); // Optional delay for effect

        // Load the game scene
        SceneManager.LoadScene("GameScene"); // Ensure this matches the name of your game scene
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // Quit the application
    }
}
