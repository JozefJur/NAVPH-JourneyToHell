using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float MaxHealth = 100;
    public float baseMaxHealth = 100;

    public float CurrentHealth = 100;
    public bool canHit = true;
    
    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;

    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth <= 0)
        {
            Debug.Log("DEAD");
            //TODO IMPLEMENT DEATH !!! (mozeme pridat item, ktory napr zabrani smrti a zmizne)
        }

        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void takeDamage(float dmg)
    {

        if (canHit)
        {
            playerAnimator.SetTrigger("takeHit");
            CurrentHealth -= dmg;
            Player.healthBar.setCurrentHealth(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                playerAnimator.SetBool("isDead", true);
                Debug.Log("DEAD");
                //TODO IMPLEMENT DEATH !!! (mozeme pridat item, ktory napr zabrani smrti a zmizne)
            }
        }

    }

    public void SetMaxHealth(float newHealth)
    {
        MaxHealth = newHealth;
    }

    public void HealObject(float health)
    {
        CurrentHealth = (CurrentHealth + health > MaxHealth) ? CurrentHealth : CurrentHealth + health;
        Player.healthBar.setCurrentHealth(CurrentHealth);
    }

    public void resetStats()
    {
        MaxHealth = baseMaxHealth;
    }
}
