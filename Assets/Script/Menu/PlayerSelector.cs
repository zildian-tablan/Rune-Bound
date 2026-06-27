using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerSelector : MonoBehaviour
{
    public string sceneName;
    public Image fadeImage;  // Assign this in the Inspector (a black Image over full screen)
    public float fadeDuration = 1.5f;

    [SerializeField]
    public bool isKnight, isMage, isArcher, isAssassin;

    public GameObject knightPrefab;  // Assign the knight prefab in the Inspector
    public GameObject magePrefab;    // Assign the mage prefab in the Inspector
                                     //public GameObject archerPrefab;  // Assign the archer prefab in the Inspector
                                     //public GameObject assassinPrefab; // Assign the assassin prefab in the Inspector

    // Called when the player selects a class
    public void PlayerSelect()
    {
        if (isKnight)
            PlayerManager.Instance.selectedClass = "Knight";
        else if (isMage)
            PlayerManager.Instance.selectedClass = "Mage";
        else if (isArcher)
            PlayerManager.Instance.selectedClass = "Archer";
        else if (isAssassin)
            PlayerManager.Instance.selectedClass = "Assassin";

        StartCoroutine(FadeAndLoad(sceneName));  // Or load Level 1 directly if skipping selector
    }
    
    public void LoadMainMenu()
{
    StartCoroutine(FadeAndLoad("MainMenu"));
}
    private void ActivateCharacter(GameObject characterPrefab)
    {
        // Deactivate all character prefabs before activating the selected one
        knightPrefab.SetActive(false);
        magePrefab.SetActive(false);
        //archerPrefab.SetActive(false);
        //assassinPrefab.SetActive(false);

        // Activate the selected character prefab
        characterPrefab.SetActive(true);
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(sceneName);
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
