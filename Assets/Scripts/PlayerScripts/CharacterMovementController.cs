using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterMovementController : MonoBehaviour
{

    public List<ItemTemplate> allItems = new List<ItemTemplate>();
    public int maxHealth = 100;
    public int currHealth;

    public HealthBarController healthBar;
    public InventoryHandler inventory;
    public CoolDownController attackCoolDown;
    public CoolDownController dashCoolDown;
    public CoolDownController sprintCoolDown;
    public CoolDownController jumpCoolDown;
    public JumpNumberController jumpNum;
    //private float dashCooldown = 0;

    private Rigidbody2D rigidBody;  // rigid body instance

    private PlayerDash playerDash;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMov;
    private PlayerJump playerJump;
    private PlayerAttack playerAttack;

   
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
        playerHealth = GetComponent<PlayerHealth>();
        playerMov = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerAttack = GetComponent<PlayerAttack>();
        /* var items = FindObjectsOfType<Object>().OfType<ItemTemplate>();
         foreach (ItemTemplate item in items)
         {
             allItems.Add(item);
         }
         //Debug.Log(allItems[0].getStacks());
         allItems[0].addStack();*/
        //Debug.Log(allItems[0].getStacks());
    }


    /*private void CalculateOnHitEffects()
    {
        foreach (ItemTemplate item in allItems)
        {
            if(item is OnHitItemTemplate)
            {
                if (item.isReady())
                {
                    var func = item.getEffectFunction();
                    func(gameObject);
                }
            }
        }
    }*/

    private void applyPositivePassiveEffects()
    {
        foreach (ItemTemplate item in allItems.ToList())
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

    private void applyNegavitvePassiveEffects()
    {
        foreach (ItemTemplate item in allItems.ToList())
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

    private void applyOtherEffects()
    {
        foreach (ItemTemplate item in allItems.ToList())
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

    private void resetStats()
    {
        playerDash.resetStats();
        playerHealth.resetStats();
        playerJump.resetStats();
        playerMov.resetStats();
        playerAttack.resetStats();
    }


    void FixedUpdate()
    {

        resetStats();

        applyPositivePassiveEffects();
        applyNegavitvePassiveEffects();

        applyOtherEffects();
        healthBar.setMaxHealth(playerHealth.MaxHealth);
        healthBar.setCurrentHealth(playerHealth.CurrentHealth);
        //Debug.Log(allItems.Count);
    }

    void Update()
    {

        foreach (ItemTemplate item in allItems)
        {
            if (!item.isReady())
            {
                item.coolDown();
            }
        }
    }

/*    private void performSprint(){
        if(Input.GetKey(KeyCode.LeftShift) && (currSpeed == movementSpeed) && (!inJump)){
            currSpeed = movementSpeed;
            movementSpeed += 25f;
        }
        else{
            movementSpeed = currSpeed;
        }
    }*/

    public void addItem(ItemTemplate item)
    {
        //Debug.Log(item is Feather);

        bool containsItem = false;

        foreach (ItemTemplate itemC in allItems)
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
            allItems.Add(item);
        }

        inventory.setItems(allItems);

    }

 //TRANSFER TO SEPARATE FILE
    public void getDamage(int dmg){
        currHealth -= dmg;
        if (currHealth <= 0){
            currHealth = 0;
        }
    }

    public void heal(int hp){
        currHealth += hp;
        if (currHealth > maxHealth){
            currHealth = maxHealth;
        }
        Debug.Log($"Curr health - {currHealth}, max health - {maxHealth}");
    }

    public void maxHealthUp(int hp){
        maxHealth += hp;

    }

    public void RemoveItem(ItemTemplate item)
    {
        foreach (ItemTemplate itemC in allItems.ToList())
        {
            if (itemC.GetType().Equals(item.GetType()))
            {
                itemC.removeStack();
                if(itemC.getStacks() == 0)
                {
                    allItems.Remove(itemC);
                }
            }
        }

        inventory.setItems(allItems);

    }
}
