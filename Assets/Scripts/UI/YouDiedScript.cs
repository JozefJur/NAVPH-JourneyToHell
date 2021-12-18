using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDiedScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}