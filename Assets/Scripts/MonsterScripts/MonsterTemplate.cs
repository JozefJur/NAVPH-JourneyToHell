using System;
using UnityEngine;

public interface MonsterTemplate
{

    int monsterCost();
    GameObject getMonster();

    int numPerSpawn();

    float spawnChance();

    //int getAttackValue();
    //int getHealth();


}
