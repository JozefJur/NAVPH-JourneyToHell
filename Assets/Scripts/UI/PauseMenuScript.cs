using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
    Source of this code: https://www.youtube.com/watch?v=JivuXdrIHK0&t=54s
    Thanks Brackeys.
*/
public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
