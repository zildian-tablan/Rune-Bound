using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSwitcher : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
            case "LevelSelect":
                AudioManager.instance.PlayMusic(AudioManager.instance.levelSelectMusic);
                break;
            case "Player Select":
                AudioManager.instance.PlayMusic(AudioManager.instance.playerSelectMusic);
                break;
            case "Level1":
            case "Level2":
            case "Level3":
                AudioManager.instance.PlayMusic(AudioManager.instance.bossMusic);
                break;
        }
    }
}
