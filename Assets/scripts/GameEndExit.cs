using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndExit : MonoBehaviour
{
    public SpriteRenderer FadeOut;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            while (FadeOut.color.a < 1.0f)
            {
                FadeOut.color = new Color(0.0f, 0.0f, 0.0f, FadeOut.color.a + Time.deltaTime * 0.4f);
                Debug.Log("Fadeout Alpha: " + FadeOut.color.a);
                yield return null;
            }

            SceneManager.LoadScene("MainMenu");
        }
    }
}
