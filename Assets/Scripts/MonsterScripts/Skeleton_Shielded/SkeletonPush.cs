using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPush : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    { 

        if (collision.gameObject.tag == "Player")
        {
            Vector2 player_velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            Debug.Log(player_velocity);
            player_velocity.x *= -2;
            Debug.Log(player_velocity);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = player_velocity;

        }
    }
}
