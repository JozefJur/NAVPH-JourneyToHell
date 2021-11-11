using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArmorPickUp : ItemPickupScript
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
        player.GetComponent<CharacterMovementController>().addItem(Activator.CreateInstance<ArmorScript>());
    }
}