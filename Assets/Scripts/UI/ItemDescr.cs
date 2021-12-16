using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDescr : MonoBehaviour
{

    public ItemPickupScript pickup;
    private ItemTemplate item;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        item = ((ItemPickupInterface) pickup).getInstanceOfTemplate();
        text.text = item.getItemDescr();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
