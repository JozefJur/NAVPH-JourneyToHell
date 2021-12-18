using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles spawn points
public class Spawn : MonoBehaviour
{
    private Camera cam;
    private bool spawned = false;
    private GameObject[] monsters;

    public GameObject stageDirector;

    private Narrator narrator;

    private bool isRegistered = false;

    private void GetAllMonsters()
    {
        monsters = Resources.LoadAll<GameObject>("Monsters");
    }


    // Start is called before the first frame update
    void Start()
    {
        GetAllMonsters();
        cam = UnityEngine.Camera.main;
        stageDirector = GameObject.FindGameObjectWithTag("Director");
        narrator = stageDirector.GetComponent<Narrator>();
    }

    // Update is called once per frame
    void Update()
    {
        // When spawnpoint is in view of camera, register
        if (CheckVisibility())
        {
            if (!isRegistered)
            {
                isRegistered = true;
                narrator.RegisterSpawnPoint(gameObject);
            }
        }
        else
        {
            // Deregister
            if (isRegistered)
            {
                isRegistered = false;
                narrator.RemoveSpawnPoint(gameObject);
            }
        }

    }
    bool CheckVisibility(){
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
