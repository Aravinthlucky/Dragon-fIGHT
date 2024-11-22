using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Singleton instance of GameManager
    private static GameManager instance;
    public static GameManager Instance => instance;

    // Player score and score UI reference
    private int playerScore = 0;
    public Text scoreText;

    // Game panels and buttons
    public GameObject gameOverPanel;       // Game Over UI panel
    public GameObject gameWonPanel;        // Game Won UI panel
    public Button replayButton;            // Replay button (for game over or won state)
    public Button nextLevelButton;         // Next Level button (for game won state)
    public GameObject pausePanel;          // Pause UI panel
    public Button pauseButton;             // Pause button
    public Button pauseReplayButton;       // Replay button (for pause panel)

    private bool isPaused = false; // To track the pause state

    private void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }

        // Initialize UI panels and set them inactive
        InitializePanels();
    }

    // Initialize or reset the UI panels
    private void InitializePanels()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameWonPanel != null) gameWonPanel.SetActive(false);
        if (replayButton != null) replayButton.gameObject.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
        if (nextLevelButton != null) nextLevelButton.gameObject.SetActive(false);
    }

    // Update method to check for escape key to toggle pause
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Method to update the player's score
    public void UpdateScore(int score)
    {
        playerScore += score;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore.ToString();
        }
    }

    // Method to trigger game over state
    public void OnPlayerDeath()
    {
        ShowGameOverPanel();
    }

    // Method to trigger game won state (when all enemies are defeated)
    public void OnEnemyDefeated()
    {
        if (AreAllEnemiesDefeated())
        {
            StartCoroutine(ShowGameWonAfterDelay(1f));
        }
    }

    // Method to return to home
    public void ReturnToHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
    }

    // Coroutine to show the Game Won panel after a delay
    private IEnumerator ShowGameWonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowGameWonPanel();
    }

    // Method to show the Game Won panel
    private void ShowGameWonPanel()
    {
        if (gameWonPanel == null)
        {
            Debug.LogError("GameWonPanel reference is missing or was destroyed!");
            return;
        }

        if (gameOverPanel != null && gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(false);
        }

        gameWonPanel.SetActive(true);
        if (nextLevelButton != null) nextLevelButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    // Method to show the Game Over panel
    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        replayButton?.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    // Method to load the next level
    public void NextLevel()
    {
        Time.timeScale = 1;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels!");
        }
    }

    // Method to check if all enemies are defeated
    private bool AreAllEnemiesDefeated()
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach (var enemy in enemies)
        {
            if (!enemy.IsDead)
            {
                return false;
            }
        }
        return true;
    }

    // Method to exit the game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Method to start the game (triggered by Play button)
    public void StartGame()
    {
        SceneManager.LoadScene("game");
        ResetGame();
    }

    // Method to replay the game (triggered by Replay button in Game Over or Game Won UI)
    public void ReplayGame()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameWonPanel != null) gameWonPanel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResetGame();
    }

    // Method to reset the game state
    private void ResetGame()
    {
        playerScore = 0;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore.ToString();
        }
        Time.timeScale = 1;
        InitializePanels();
    }

    // Method to resume the game from the pause panel
    public void ResumeFromPause()
    {
        HidePausePanel();
        isPaused = false;
        Time.timeScale = 1;
    }

    // Method to toggle pause state
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ShowPausePanel();
        }
        else
        {
            HidePausePanel();
        }
    }

    // Method to show the Pause Panel
    private void ShowPausePanel()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
        Time.timeScale = 0;
    }

    // Method to hide the Pause Panel
    private void HidePausePanel()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        Time.timeScale = 1;
    }

    // Method to restart the game from the pause menu
    public void RestartGame()
    {
        HidePausePanel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResetGame();
    }

    // Method to return to home from the pause menu
    public void ReturnToHomeFromPause()
    {
        HidePausePanel();
        ReturnToHome();
    }
}
