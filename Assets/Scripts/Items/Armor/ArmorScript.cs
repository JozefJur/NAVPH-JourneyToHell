using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArmorScript : PassiveItemTemplate
{
    public float coolDownTime = 0f;

    public float maxHealthBoost = 0.1f;
    public float movementSpeedPenalty = 0.05f;

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

            PlayerHealth scriptH = x.GetComponent<PlayerHealth>();

            scriptH.SetMaxHealth(scriptH.baseMaxHealth + ((getStacks() * maxHealthBoost) * scriptH.baseMaxHealth));
        };
    }

    public Action<GameObject> getNegativeEffectFunction()
    {
        return (GameObject x) =>
        {

            PlayerMovement scriptM = x.GetComponent<PlayerMovement>();

            scriptM.movementSpeed -= ((getStacks() * movementSpeedPenalty) * scriptM.baseMovementSpeed);
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
        return ItemAssetsHolder.Instance.armorSprite;
    }

    public string getItemDescr()
    {
        return "Good Effects: Each stack adds " + (100 * maxHealthBoost) + " max health\n" +
        "Bad Effects: Each stack removes " + (movementSpeedPenalty * 15) + " movement speed";
    }
}
