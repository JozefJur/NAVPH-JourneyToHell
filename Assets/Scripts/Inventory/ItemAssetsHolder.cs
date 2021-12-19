using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssetsHolder : MonoBehaviour
{
    public static ItemAssetsHolder Instance{get; private set;}

    void Awake()
    {
        Instance = this;    
    }

    public Sprite armorSprite;
    public Sprite featherSprite;
    public Sprite firstAidSprite;
    public Sprite redbullSprite;
    public Sprite steroidsSprite;
}