using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemGiver : MonoBehaviour
{

    public List<GameObject> itemsCommon;

    public List<GameObject> itemsUncommon;

    public List<GameObject> itemsRare;

    public List<GameObject> itemsLegendary;

    private void getAllItems()
    {
        GameObject[] items = Resources.LoadAll<GameObject>("Items");

        foreach(GameObject item in items)
        {
            ItemPickupInterface itemScript = item.GetComponent<ItemPickupInterface>();
            ItemTemplate itemInstance = itemScript.getInstanceOfTemplate();
            switch (itemInstance.getRarity())
            {
                case ItemTemplate.Rarity.COMMON:
                    itemsCommon.Add(item);
                    break;
                case ItemTemplate.Rarity.UNCOMMON:
                    itemsUncommon.Add(item);
                    break;
                case ItemTemplate.Rarity.RARE:
                    itemsRare.Add(item);
                    break;
                case ItemTemplate.Rarity.LEGENDARY:
                    itemsLegendary.Add(item);
                    break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        getAllItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getRandomItem()
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
                        return itemsCommon[UnityEngine.Random.Range(0, itemsCommon.Count)];
                    case ItemTemplate.Rarity.UNCOMMON:
                        return itemsCommon[UnityEngine.Random.Range(0, itemsCommon.Count)];
                    // return itemsUncommon[UnityEngine.Random.Range(0, itemsCommon.Count -1)];
                    case ItemTemplate.Rarity.RARE:
                        return itemsCommon[UnityEngine.Random.Range(0, itemsCommon.Count)];
                    // return itemsRare[UnityEngine.Random.Range(0, itemsCommon.Count-1)];
                    case ItemTemplate.Rarity.LEGENDARY:
                        return itemsCommon[UnityEngine.Random.Range(0, itemsCommon.Count)];
                        // return itemsLegendary[UnityEngine.Random.Range(0, itemsCommon.Count-1)];
                }
            }
        }
        return null;
    }
}
