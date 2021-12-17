using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Steroids : PassiveItemTemplate
{

    public float coolDownTime = 0f;

    public float attackDamageBoost = 0.1f;

    public float attackSpeedPenalty = 0.025f;

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

            PlayerAttack script = x.GetComponent<PlayerAttack>();

            //script.currentDmg = script.baseDmg + (script.baseDmg * getStacks() * attackDamageBoost);
            script.currentDmg = script.baseDmg * (float)(Math.Pow((1 + attackDamageBoost),getStacks()));

        };
    }

    public Action<GameObject> getNegativeEffectFunction()
    {
        return (GameObject x) =>
        {
            PlayerAttack script = x.GetComponent<PlayerAttack>();

            //script.currentAttackSpeed -= (script.baseAttackSpeed * getStacks() * attackSpeedPenalty);
            script.currentAttackSpeed *= (float)( Math.Pow((1-attackSpeedPenalty), getStacks()));
        };
    }

    public void coolDown()
    {
        if (getCoolDown() <= 0)
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
        return ItemAssetsHolder.Instance.steroidsSprite;
    }

    public string getItemDescr()
    {
        return "Good Effects: Each stack adds " + (40 * attackDamageBoost) + " damage\n" +
        "Bad Effects: Each stack removes " + attackSpeedPenalty  + " attack speed";
    }
}
