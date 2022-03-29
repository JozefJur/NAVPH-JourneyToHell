using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public static int NumberOfKilledMonsters = 0;
    public static int NumberOfCollectedItems = 0;
    public static float DamageRecieved = 0;
    public static float DamageDealt = 0;
    public static bool Won = false;

    public static void ResetStatistics()
    {
        NumberOfCollectedItems = 0;
        NumberOfKilledMonsters = 0;
        DamageDealt = 0;
        DamageRecieved = 0;
        Won = false;
    }

    public static int GetTotalScore()
    {
        return 0;
    }

}
