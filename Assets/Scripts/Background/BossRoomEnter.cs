using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script is used to create Wall behind player when boss room is enterred.
public class BossRoomEnter : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if(Wall != null)
            {
                Wall.transform.gameObject.SetActive(true);
                Boss.SetActive(true);
                Narrator narrator = GameObject.FindGameObjectWithTag("Director").GetComponent<Narrator>();
                narrator.DestroyAllEnemies();
            }
            
        }
    }
}
