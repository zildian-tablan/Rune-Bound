using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign your Pause Panel in the inspector
    private bool isPaused = false;

    void Update()
    {
        // Optional: Keyboard pause toggle
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause game
            pauseMenuUI.SetActive(true);
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume game
        pauseMenuUI.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Ensure time is normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Ensure time is normal
        SceneManager.LoadScene("MainMenu"); // Replace with your menu scene name
    }
}
