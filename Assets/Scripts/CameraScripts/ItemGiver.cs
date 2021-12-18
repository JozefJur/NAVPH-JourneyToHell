using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Script stores all items with their raritty
public class ItemGiver : MonoBehaviour
{

    public List<GameObject> ItemsCommon;

    public List<GameObject> ItemsUncommon;

    public List<GameObject> ItemsRare;

    public List<GameObject> ItemsLegendary;

    private void GetAllItems()
    {
        GameObject[] items = Resources.LoadAll<GameObject>("Items");

        foreach(GameObject item in items)
        {
            ItemPickupInterface itemScript = item.GetComponent<ItemPickupInterface>();
            ItemTemplate itemInstance = itemScript.getInstanceOfTemplate();
            switch (itemInstance.getRarity())
            {
                case ItemTemplate.Rarity.COMMON:
                    ItemsCommon.Add(item);
                    break;
                case ItemTemplate.Rarity.UNCOMMON:
                    ItemsUncommon.Add(item);
                    break;
                case ItemTemplate.Rarity.RARE:
                    ItemsRare.Add(item);
                    break;
                case ItemTemplate.Rarity.LEGENDARY:
                    ItemsLegendary.Add(item);
                    break;
            }
        }
    }
    // Get all items from resources on start
    void Start()
    {
        GetAllItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function returns random item by their chance
    public GameObject GetRandomItem()
    {
        int number = UnityEngine.Random.Range(0, 100);
        //Debug.Log(number);
        foreach(ItemTemplate.Rarity rarity in Enum.GetValues(typeof(ItemTemplate.Rarity))){
            //Debug.Log(number + " " + (int) rarity);
            if (number <= (int)rarity)
            {
                switch (rarity)
                {
                    case ItemTemplate.Rarity.COMMON:
                        return ItemsCommon[UnityEngine.Random.Range(0, ItemsCommon.Count)];
                    case ItemTemplate.Rarity.UNCOMMON:
                        return ItemsCommon[UnityEngine.Random.Range(0, ItemsCommon.Count)];
                    // return itemsUncommon[UnityEngine.Random.Range(0, itemsCommon.Count -1)];
                    case ItemTemplate.Rarity.RARE:
                        return ItemsCommon[UnityEngine.Random.Range(0, ItemsCommon.Count)];
                    // return itemsRare[UnityEngine.Random.Range(0, itemsCommon.Count-1)];
                    case ItemTemplate.Rarity.LEGENDARY:
                        return ItemsCommon[UnityEngine.Random.Range(0, ItemsCommon.Count)];
                        // return itemsLegendary[UnityEngine.Random.Range(0, itemsCommon.Count-1)];
                }
            }
        }
        return null;
    }
}
