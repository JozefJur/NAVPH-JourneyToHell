using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class YouDiedScript : MonoBehaviour
{    
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Status;

    private void Start() {
        SetStatisticsText();    
    }

    public void StartGame()
    {
        PauseMenuScript.GameIsPaused = false;
        PlayerStatistics.ResetStatistics();
        SceneManager.LoadSceneAsync("Game");
    }
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public void SetStatisticsText()
    {
        if(PlayerStatistics.Won){
            Status.SetText("You won!");
        }
        else{
             Status.SetText("You died!");
        }
        Score.SetText("Number of killed enemies\t" + PlayerStatistics.NumberOfKilledMonsters + 
                    "\nNumber of collected items\t" + PlayerStatistics.NumberOfCollectedItems + 
                    "\nDamage received\t\t\t" + (int)(PlayerStatistics.DamageRecieved) + 
                    "\nDamage dealt\t\t\t" + (int)(PlayerStatistics.DamageDealt));
    }
}
