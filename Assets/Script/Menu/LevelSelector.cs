using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour
{
    public string sceneName;
    public Image fadeImage;  // Assign this in the Inspector (a black Image over full screen)
    public float fadeDuration = 1.5f;

    public void LevelSelect()
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string levelName)
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(levelName);
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
