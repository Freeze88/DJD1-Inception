using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject startScreen;
    GameObject mainMenu;
    GameObject options;
    AudioSource audio;
    public SpriteRenderer FadeOut;
    bool isFullscreen = true;
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        startScreen = GameObject.Find("StartScreen");
        mainMenu = GameObject.Find("MainMenu");
        options = GameObject.Find("OptionWindow");

        mainMenu.SetActive(false);
        options.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Input.anyKeyDown)
        {
            startScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        StartCoroutine(volume());
    }

    public void Options()
    {
        options.SetActive(!options.activeSelf);
    }

    public void ChangeAudio(float newAudio)
    {
        if (audio != null)
            audio.volume = newAudio;
    }

    public void ChangeResolution(int resolution)
    {
        switch (resolution)
        {
            case 0: Screen.SetResolution(1980, 1080, isFullscreen); break;
            case 1: Screen.SetResolution(1280, 720, isFullscreen); break;
            case 2: Screen.SetResolution(960, 540, isFullscreen); break;
        }
    }
    public void Fullscreen(bool on)
    {
        isFullscreen = on;
        Screen.fullScreen = isFullscreen;
    }
    
    public IEnumerator volume()
    {
        while (audio.volume > 0.0f)
        {
            audio.volume = audio.volume - Time.deltaTime * 0.2f;
            FadeOut.color = new Color(0.0f, 0.0f, 0.0f, FadeOut.color.a + Time.deltaTime * 0.2f);
            yield return null;
        }
        SceneManager.LoadScene("AddShapeshift");
    }
}
