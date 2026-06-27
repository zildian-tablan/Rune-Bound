using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeCanvas(1, 0)); // Fade out from black
    }

    public void FadeToLevel(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        yield return StartCoroutine(FadeCanvas(0, 1)); // Fade in to black
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeCanvas(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        canvasGroup.alpha = startAlpha;
        canvasGroup.blocksRaycasts = true; 

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}