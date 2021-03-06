using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Text;
using TMPro;

// Script is used to handle item pickup, this script is then extended for each item to implement custom logic
public class ItemPickupScript : MonoBehaviour, ItemPickupInterface
{
    
    protected bool canPickUp = false;
    public Canvas itemDescr;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Can pick up, when close to the item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canPickUp = true;
            if(itemDescr != null)
            {
                itemDescr.transform.gameObject.SetActive(true);

            }
        }
    }

    // Remove the ability to pick up
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canPickUp = false;
            itemDescr.transform.gameObject.SetActive(false);
        }
    }

    public virtual ItemTemplate getInstanceOfTemplate()
    {
        throw new NotImplementedException();
    }
}
