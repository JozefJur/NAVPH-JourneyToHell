using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Feather : PassiveItemTemplate
{

    public float coolDownTime = 0f;

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

    public Action<CharacterMovementController> getEffectFunction()
    {
        return (CharacterMovementController x) =>
        {
            x.jumpNumber = getStacks() + 1;
            currentCoolDownTime = coolDownTime;
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

}
