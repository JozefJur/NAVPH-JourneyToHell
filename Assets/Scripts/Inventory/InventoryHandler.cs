using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{

    private List<ItemTemplate> playerItems;
    private List<Transform> iconList = new List<Transform>();
    private Transform inventoryContainer;
    private Transform itemTemplate;

    void Awake()
    {
        inventoryContainer = transform.Find("ItemSlotsContainer");
        itemTemplate = inventoryContainer.Find("ItemContainerTemplate");
    }

    public void setItems(List<ItemTemplate> items)
    {
        this.playerItems = items;
        DrawInventory();
    }

    private void DrawInventory()
    {

        int num = 0;
        float size = 50f;
        float space = 10f;

        foreach(Transform invItem in iconList)
        {
            Destroy(invItem.gameObject);
        }

        iconList.Clear();

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
            itemSlot.anchoredPosition = new Vector2(num * (size + space), 0);
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
