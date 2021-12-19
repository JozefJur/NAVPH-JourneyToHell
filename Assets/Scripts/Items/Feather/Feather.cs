using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Feather : PassiveItemTemplate
{

    public float coolDownTime = 0f;

    public int jumpNumBoost = 1;

    private float currentCoolDownTime = 0f;

    private ItemTemplate.State currState = ItemTemplate.State.READY;

    private int stacks = 0;

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

    public Action<GameObject> getPositiveEffectFunction()
    {
        return (GameObject x) =>
        {

            PlayerJump script = x.GetComponent<PlayerJump>();

            script.SetJumpNumber(getStacks() + jumpNumBoost);

        };
    }

    public Action<GameObject> getNegativeEffectFunction()
    {
        return (GameObject x) =>
        {
            return;
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
        return ItemAssetsHolder.Instance.featherSprite;
    }

    public string getItemDescr()
    {
        return "Good Effects: Each stack adds one jump\n";
    }
}
