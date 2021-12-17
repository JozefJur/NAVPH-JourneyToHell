using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnter : MonoBehaviour
{
    public GameObject wall;
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
            Debug.Log("som tu");
            if(wall != null)
            {
                wall.transform.gameObject.SetActive(true);
                Boss.SetActive(true);
                Narrator narrator = GameObject.FindGameObjectWithTag("Director").GetComponent<Narrator>();
                narrator.destroyAllEnemies();
            }
            
        }
    }
}
