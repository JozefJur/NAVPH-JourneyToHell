using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class RedBullPickUp : ItemPickupScript
{
    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            pickItem();
        }
    }

    public void pickItem()
    {
        Destroy(gameObject);
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.GetComponent<CharacterMovementController>().addItem(Activator.CreateInstance<RedBull>());
    }

}
