using System;
using UnityEngine;

// Interface defines base monster properties
public interface MonsterTemplate
{

    int monsterCost();

    // Return gameObject instance
    GameObject getMonster();

    // Number of monsters per spawn
    int numPerSpawn();

    // Percentage chance to spawn this type
    float spawnChance();

    //int getAttackValue();
    //int getHealth();


}
