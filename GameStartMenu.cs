using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartMenu : MonoBehaviour
{
    public GameObject startMenuPanel; // The game start panel with Play and Exit buttons

    private void Start()
    {
        // Ensure the start menu panel is active when the game starts
        startMenuPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game until the Play button is pressed
    }

    // Method to handle the Play button click
    public void StartGame()
    {
        startMenuPanel.SetActive(false); // Hide the start panel
        Time.timeScale = 1; // Resume the game
    }

    // Method to handle the Exit button click
    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit(); // Quit the application
    }
}
