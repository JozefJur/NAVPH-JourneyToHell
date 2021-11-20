using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour, MonsterTemplate
{

    public GameObject goblin;
    public int price;

    public GameObject getMonster()
    {
        return goblin;
    }

    public int monsterCost()
    {
        return price;
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
