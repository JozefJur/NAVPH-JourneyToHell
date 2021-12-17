using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour, MonsterTemplate
{

    public GameObject skeleton;
    public int price;
    public int numberPerSpawn;

    public GameObject getMonster()
    {
        return skeleton;
    }

    public int monsterCost()
    {
        return price;
    }

    public int numPerSpawn()
    {
        return numberPerSpawn;
    }

    public float spawnChance()
    {
        return 40;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
