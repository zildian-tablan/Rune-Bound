using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeSlider.value = savedVolume;

        // Add listener to update AudioManager on slider move
        volumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.instance.SetVolume(value);
        });
    }
}