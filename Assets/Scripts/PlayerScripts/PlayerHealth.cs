using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles player health
public class PlayerHealth : MonoBehaviour
{

    public float MaxHealth = 100;
    public float BaseMaxHealth = 100;
    public float CurrentHealth = 100;
    public bool CanHit = true;

    
    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;

    private Animator playerAnimator;

    // Base initialization
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(CurrentHealth <= 0)
        {
            Debug.Log("DEAD");
        }

        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void TakeDamage(float dmg)
    {

        if (CanHit)
        {
            playerAnimator.SetTrigger("takeHit");
            CurrentHealth -= dmg;
            Player.healthBar.setCurrentHealth(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                playerAnimator.SetBool("isDead", true);
                Debug.Log("DEAD");
               
            }
        }

    }

    public void SetMaxHealth(float newHealth)
    {
        MaxHealth = newHealth < 20f ? 20f : newHealth;
    }

    public void HealObject(float health)
    {
        CurrentHealth = (CurrentHealth + health > MaxHealth) ? CurrentHealth : CurrentHealth + health;
        Player.healthBar.setCurrentHealth(CurrentHealth);
    }

    public void ResetStats()
    {
        MaxHealth = BaseMaxHealth;
    }
}
