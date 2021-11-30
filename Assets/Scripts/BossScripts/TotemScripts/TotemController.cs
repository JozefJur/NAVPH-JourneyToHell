using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemController : MonoBehaviour
{

    private MonsterHealth health;
    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<MonsterHealth>();
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if(health.CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
