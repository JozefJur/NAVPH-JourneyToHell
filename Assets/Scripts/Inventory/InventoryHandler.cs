using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Script handles inventory UI
public class InventoryHandler : MonoBehaviour
{

    private List<ItemTemplate> playerItems;
    private List<Transform> iconList = new List<Transform>();
    private Transform inventoryContainer;
    private Transform itemTemplate;
    private float beginOffset = 37f;

    void Awake()
    {
        inventoryContainer = transform.Find("ItemSlotsContainer");
        itemTemplate = inventoryContainer.Find("ItemContainerTemplate");
    }

    // Set player items
    public void SetItems(List<ItemTemplate> items)
    {
        this.playerItems = items;
        DrawInventory();
    }

    // Function draws all items in inventory to the UI screen
    private void DrawInventory()
    {

        int num = 0;
        float size = 40f;
        float space = 20f;

        foreach(Transform invItem in iconList)
        {
            Destroy(invItem.gameObject);
        }

        // Clear current icon list
        iconList.Clear();

        // Create image for each item type and add number of stacks
        foreach (ItemTemplate item in playerItems)
        {
            if(item is FirstAidKit)
            {
                continue;
            }
            Transform obj = Instantiate(itemTemplate, inventoryContainer);
            iconList.Add(obj);
            RectTransform itemSlot = obj.GetComponent<RectTransform>();

            itemSlot.gameObject.SetActive(true);
            itemSlot.anchoredPosition = new Vector2(beginOffset + num * (size + space), 0);
            Image img = itemSlot.Find("ItemImage").GetComponent<Image>();
            img.sprite = item.getSprite();
            TextMeshProUGUI imgText = itemSlot.Find("AmountText").GetComponent<TextMeshProUGUI>();
            imgText.SetText(item.getStacks() + "");
            num++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
