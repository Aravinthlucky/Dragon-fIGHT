using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;  // The loading screen UI panel
    public GameObject mainMenuPanel;  // The main menu panel
    public Slider progressBar;        // Slider to show loading progress
    public Text progressText;         // (Optional) Text to display loading percentage
    public float loadingDuration = 4f; // Time in seconds to display the loading screen

    

    private void Start()
    {
        // Ensure the loading screen is hidden initially
        loadingScreen.SetActive(false);
    }

    // Method to start loading the game
    public void StartGame(string sceneName)
    {
        // Hide the main menu
        mainMenuPanel.SetActive(false);

        // Show the loading screen
        loadingScreen.SetActive(true);

        // Start the coroutine to load the game scene
        StartCoroutine(LoadGameSceneWithDelay(sceneName));
    }

    // Coroutine to handle the loading of the game scene
    IEnumerator LoadGameSceneWithDelay(string sceneName)
    {
        // Begin loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Prevent the scene from activating immediately
        operation.allowSceneActivation = false;

        // Timer for 4 seconds (loading screen duration)
        float elapsedTime = 0f;

        // While the scene is loading (operation.progress < 0.9)
        while (operation.progress < 0.9f)
        {
            // Calculate the progress and update the progress bar
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.value = progress;
            }
            if (progressText != null)
            {
                progressText.text = Mathf.RoundToInt(progress * 100) + "%";
            }

            yield return null; // Wait for the next frame
        }

        // Wait until 4 seconds have passed
        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;

            // Optionally update UI during this period (e.g., keep progress bar full)
            if (progressBar != null)
            {
                progressBar.value = 1f; // Full progress after loading completes
            }
            if (progressText != null)
            {
                progressText.text = "100%";
            }

            yield return null; // Wait for the next frame
        }

        // After 4 seconds, allow the scene to activate
        operation.allowSceneActivation = true;
    }
}
