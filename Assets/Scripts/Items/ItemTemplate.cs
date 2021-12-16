using System;
using UnityEngine;

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

    Action<GameObject> getPositiveEffectFunction();

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
