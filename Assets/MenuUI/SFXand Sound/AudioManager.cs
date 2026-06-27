using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip levelSelectMusic;
    public AudioClip bossMusic;
    public AudioClip playerSelectMusic;

    public Slider slider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SetVolume(savedVolume);

        if (slider != null)
            slider.value = savedVolume;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps music playing across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // Don't restart same track
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

}
