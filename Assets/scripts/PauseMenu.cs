using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public SpriteRenderer FadeOut;
    GameObject pauseMenu;
    bool pause = false;

    void Start()
    {
        pauseMenu = GameObject.Find("Canvas");
        pauseMenu.gameObject.SetActive(false);
    }

    void Update()
    {
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
