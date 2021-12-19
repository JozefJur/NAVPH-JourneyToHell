using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles camera movement with player
public class CameraScript : MonoBehaviour
{

    public Transform Player;
    public Vector3 Offset; // 0 10 -64

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(Player.position.x + Offset.x, Player.position.y + Offset.y, Offset.z);
    }
}
