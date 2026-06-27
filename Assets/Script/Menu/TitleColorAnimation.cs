using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleColorAnimation : MonoBehaviour
{
    //public Text titleText; // Use this for UI.Text
    public TextMeshProUGUI titleText; // Use this for TMP instead

    public Color[] colors;
    public float changeInterval = 1f;

    private int currentColorIndex = 0;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            titleText.color = colors[currentColorIndex];
            timer = 0f;
        }
    }
}
