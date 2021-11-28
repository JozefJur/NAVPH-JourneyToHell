using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private Camera cam;
    private bool spawned = false;
    private GameObject[] monsters;



    private void getAllMonsters()
    {
        monsters = Resources.LoadAll<GameObject>("Monsters");
    }


    // Start is called before the first frame update
    void Start()
    {
        getAllMonsters();
        cam = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkVisibility()&&!spawned)
        {
            spawned = true;
            double timeSinceStartup = Time.realtimeSinceStartup;
            int length = (int)(timeSinceStartup / 300);
            Debug.Log(length);
            for(int i = 0; i < length + 1; i++){
                Instantiate(monsters[Random.Range(0, monsters.Length - 1)], transform.position, Quaternion.identity);
            }
            Delete();
        }
    }
    bool checkVisibility(){
         Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Delete()
    {
        Destroy(gameObject);
    }

}
