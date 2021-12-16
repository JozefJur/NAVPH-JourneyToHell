using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstAidKit : PassiveItemTemplate
{
    
    public float coolDownTime = 0f;

    private float currentCoolDownTime = 0f;

    private ItemTemplate.State currState = ItemTemplate.State.READY;

    private int stacks = 0;

    private int hp = 10;

    //private bool canPickUp = false;
    

    public void addStack()
    {
        stacks++;
    }

    public int getStacks()
    {
        return stacks;
    }

    public void removeStack()
    {
        stacks--;
    }

    public bool isReady()
    {
        return currState == ItemTemplate.State.READY;
    }

    public float getCoolDown()
    {
        return currentCoolDownTime;
    }

    public ItemTemplate.State getState()
    {
        return currState;
    }

/*    public Action<CharacterMovementController> getEffectFunction()
    {
        return (CharacterMovementController x) =>
        {
            x.heal(hp);
            currentCoolDownTime = coolDownTime;
        };
    }*/

    public Action<GameObject> getPositiveEffectFunction()
    {
        return (GameObject x) =>
        {

            PlayerHealth script = x.GetComponent<PlayerHealth>();

            script.HealObject(hp);

        };
    }

    public Action<GameObject> getNegativeEffectFunction()
    {
        return (GameObject x) =>
        {
            CharacterMovementController playerScript = x.GetComponent<CharacterMovementController>();
            playerScript.RemoveItem(this);
        };
    }

    public void coolDown()
    {
        if(getCoolDown() <= 0)
        {
            currState = ItemTemplate.State.READY;
            currentCoolDownTime = 0f;
        }
        else
        { 
            currentCoolDownTime -= Time.deltaTime;
        }
    }

    public ItemTemplate.Rarity getRarity()
    {
        return ItemTemplate.Rarity.COMMON;
    }

    public Sprite getSprite()
    {
        return ItemAssetsHolder.Instance.firstAidSprite;
    }

    public string getItemDescr()
    {
        return "This item heals Player for " + hp + " health";
    }

}
