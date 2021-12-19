using System;
using UnityEngine;


// Interface is used to define all methods for items
public interface ItemTemplate
{

    void addStack();
    int getStacks();
    void removeStack();
    bool isReady();
    float getCoolDown();
    void coolDown();
    Rarity getRarity();

    State getState();

    Sprite getSprite();

    string getItemDescr();

    // Function applies positive effect to player
    Action<GameObject> getPositiveEffectFunction();

    // Function applies negative effect to player
    Action<GameObject> getNegativeEffectFunction();

    public enum Rarity
    {
        COMMON = 60,
        UNCOMMON = 25,
        RARE = 10,
        LEGENDARY = 2
    }

    public enum State
    {
        READY,
        COOLDOWN
    }
}
