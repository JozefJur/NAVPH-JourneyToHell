using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Script handles all player actions
public class CharacterMovementController : MonoBehaviour
{

    public List<ItemTemplate> AllItems = new List<ItemTemplate>();

    public HealthBarController healthBar;
    public InventoryHandler inventory;
    public CoolDownController attackCoolDown;
    public CoolDownController dashCoolDown;
    public CoolDownController sprintCoolDown;
    public CoolDownController jumpCoolDown;
    public JumpNumberController jumpNum;
    public CoolDownController heavyAttackCoolDown;
    //private float dashCooldown = 0;

    private Rigidbody2D rigidBody;  // rigid body instance

    private PlayerDash playerDash;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMov;
    private PlayerJump playerJump;
    private PlayerAttack playerAttack;

   
    // Base initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
        playerHealth = GetComponent<PlayerHealth>();
        playerMov = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerAttack = GetComponent<PlayerAttack>();

    }

    // Function applies all positive effects from gathered items to player
    private void ApplyPositivePassiveEffects()
    {
        foreach (ItemTemplate item in AllItems.ToList())
        {
            if (item is PassiveItemTemplate)
            {
                if (item.isReady())
                {
                    item.getPositiveEffectFunction()(gameObject);
                }
            }
        }
    }
    // Function applies all negative effects from gathered items to player
    private void ApplyNegavitvePassiveEffects()
    {
        foreach (ItemTemplate item in AllItems.ToList())
        {
            if (item is PassiveItemTemplate)
            {
                if (item.isReady())
                {
                    item.getNegativeEffectFunction()(gameObject);
                }
            }
        }
    }
    // Function applies all positive effects from gathered items to player
    private void ApplyOtherEffects()
    {
        foreach (ItemTemplate item in AllItems.ToList())
        {
            if (!(item is PassiveItemTemplate))
            {
                if ( ((ActiveItemTemplate) item).isActive())
                {
                    item.getPositiveEffectFunction()(gameObject);
                    item.getNegativeEffectFunction()(gameObject);
                }
            }
        }
    }

    // Function resets all stats to base value
    private void ResetStats()
    {
        playerDash.ResetStats();
        playerHealth.ResetStats();
        playerJump.ResetStats();
        playerMov.ResetStats();
        playerAttack.ResetStats();
    }

    // Reset all stats and apply effects from items
    void FixedUpdate()
    {

        ResetStats();

        ApplyPositivePassiveEffects();
        ApplyNegavitvePassiveEffects();

        ApplyOtherEffects();
        healthBar.setMaxHealth(playerHealth.MaxHealth);
        healthBar.setCurrentHealth(playerHealth.CurrentHealth);
        //Debug.Log(allItems.Count);
    }

    void Update()
    {

        foreach (ItemTemplate item in AllItems)
        {
            if (!item.isReady())
            {
                item.coolDown();
            }
        }
    }

    // Function adds item to inventory
    public void AddItem(ItemTemplate item)
    {

        PlayerStatistics.NumberOfCollectedItems += 1;
        //Debug.Log(item is Feather);

        bool containsItem = false;

        foreach (ItemTemplate itemC in AllItems)
        {
            if (itemC.GetType().Equals(item.GetType()))
            {
                itemC.addStack();
                containsItem = true;
            }
        }

        if (!containsItem)
        {
            item.addStack();
            AllItems.Add(item);
        }

        inventory.SetItems(AllItems);

    }

    // Function removes item from inventory
    public void RemoveItem(ItemTemplate item)
    {
        foreach (ItemTemplate itemC in AllItems.ToList())
        {
            if (itemC.GetType().Equals(item.GetType()))
            {
                itemC.removeStack();
                if(itemC.getStacks() == 0)
                {
                    AllItems.Remove(itemC);
                }
            }
        }

        inventory.SetItems(AllItems);

    }
}
