using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour
{
    public List<GameObject> Levels;
    public GameObject CurrLevel;
    public GameObject LevelPosition1;
    public GameObject LevelPosition2;
    
    // If false -> position1, else if true -> position2
    private bool position;
    
    // Start is called before the first frame update
    void Start()
    {
        InstantiateLevel(0, GameObject.FindGameObjectWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InstantiateLevel(int index, GameObject player)
    {
        GameObject oldLevel = CurrLevel;
        if(oldLevel != null)
        {
            DeleteOldLevel(oldLevel);
        }
        CurrLevel = Instantiate(Levels[index], position?LevelPosition2.transform.position:LevelPosition1.transform.position, Quaternion.identity);
        player.transform.position = CurrLevel.transform.Find("StartPoint").transform.position;
        player.transform.position= new Vector3(player.transform.position.x,player.transform.position.y,-2.5f);
        
        position = !position;
    }
    public void DeleteOldLevel(GameObject level)
    {
        Debug.Log("destroy level");
        DestroyImmediate(level);
    }
}
