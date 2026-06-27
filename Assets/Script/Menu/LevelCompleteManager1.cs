using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteManager1 : MonoBehaviour
{

    public void LoadNextLevel2()
    {
        Time.timeScale = 1f; // Ensure time is normal
        SceneManager.LoadScene("Level2"); // Replace with your menu scene name
    }
    public void LoadNextLevel3()
    {
        Time.timeScale = 1f; // Ensure time is normal
        SceneManager.LoadScene("Level3"); // Replace with your menu scene name
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

    public void LoadCredits()
    {
        Time.timeScale = 1f; // Ensure time is normal
        SceneManager.LoadScene("Credits"); // Replace with your menu scene name
    }
}
