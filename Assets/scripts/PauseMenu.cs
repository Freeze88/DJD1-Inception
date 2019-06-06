using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public SpriteRenderer FadeOut;
    GameObject pauseMenu;
    bool pause = false;
    public Shapeshift shapeshift;
    public Image Timer;

    void Start()
    {
        pauseMenu = GameObject.Find("Canvas");
        pauseMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateTimer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            pauseMenu.gameObject.SetActive(pause);
        }
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else if (!pause)
        {
            Time.timeScale = 1;
        }
    }

    public void QuitToMenu()
    {
        pause = false;
        StartCoroutine(Exiting());
    }

    public void UpdateTimer()
    {
        Debug.Log("Here");
        Debug.Log(shapeshift.GetTimer());
        Timer.fillAmount = shapeshift.GetTimer();
    }

    public IEnumerator pauses()
    {
        yield return new WaitForSeconds(5);
    }
    public IEnumerator Exiting()
    {
        while (FadeOut.color.a < 1.0f)
        {
            FadeOut.color = new Color(0.0f, 0.0f, 0.0f, FadeOut.color.a + Time.deltaTime * 0.4f);
            yield return null;
        }
        
        SceneManager.LoadScene("MainMenu");
    }
}
