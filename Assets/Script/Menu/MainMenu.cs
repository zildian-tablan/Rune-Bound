using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CrossFade fade;
    public void Play()
    {
        fade.FadeToLevel("Player Select");
    }
    public void Options()
    {
        fade.FadeToLevel("About");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
