using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set player canClimb flag to true
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerClimb>().CanClimb = true;
        }
    }

    // Set player canClimb flag to false
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerClimb>().CanClimb = false;
        }
    }
}
