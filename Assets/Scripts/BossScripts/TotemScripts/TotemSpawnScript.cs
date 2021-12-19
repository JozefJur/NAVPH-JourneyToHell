using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script is used to spawn Totem
public class TotemSpawnScript : MonoBehaviour
{

    public GameObject Totem;
    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnTotem()
    {
        return Instantiate(Totem, spawnPosition, Quaternion.identity);
    }
}
