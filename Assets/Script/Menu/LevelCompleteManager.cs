using UnityEngine;
using System.Collections;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompletePanel;
    public float delayBeforePanel = 2f;
    public AudioSource victorySound; // optional

    public void ShowLevelComplete()
    {
        StartCoroutine(DelayedLevelComplete());
    }

    private IEnumerator DelayedLevelComplete()
    {
        if (victorySound != null)
        {
            victorySound.Play();
        }

        yield return new WaitForSeconds(delayBeforePanel);

        levelCompletePanel.SetActive(true);
        Time.timeScale = 0f; // optional pause
    }
}
