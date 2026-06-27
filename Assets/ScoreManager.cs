using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int currentScore;

    public TextMeshProUGUI scoreText;// UI Text element to display the score

    private void Awake()
    {
        currentScore = 0;
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    public int GetScore()
    {
        return currentScore;
    }
}
