using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
