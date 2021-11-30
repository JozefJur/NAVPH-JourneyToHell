using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemSpawnScript : MonoBehaviour
{

    public GameObject totem;
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
        return Instantiate(totem, spawnPosition, Quaternion.identity);
    }
}
