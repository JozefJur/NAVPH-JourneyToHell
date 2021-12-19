using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public void StartGame()
    {
        PauseMenuScript.GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Game");
    }

    /*public void ShowOptions()
    {

    }

    public void ShowHowToPlay()
    {

    }*/

    public void ExitGame()
    {
        Application.Quit();
    }
}
