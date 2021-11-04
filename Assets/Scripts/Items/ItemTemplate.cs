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

    Action<GameObject> getPositiveEffectFunction();

    Action<GameObject> getNegativeEffectFunction();

    public enum Rarity
    {
        COMMON = 33,
        UNCOMMON = 20,
        RARE = 10,
        LEGENDARY = 3
    }

    public enum State
    {
        READY,
        COOLDOWN
    }
}
