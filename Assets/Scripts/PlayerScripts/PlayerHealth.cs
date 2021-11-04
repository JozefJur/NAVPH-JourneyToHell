using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float MaxHealth = 100;
    public float baseMaxHealth = 100;

    public float CurrentHealth = 100;
    
    
    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
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
        CurrentHealth -= dmg;
    }

    public void SetMaxHealth(float newHealth)
    {
        MaxHealth = newHealth;
    }

    public void HealObject(float health)
    {
        CurrentHealth = (CurrentHealth + health > MaxHealth) ? CurrentHealth : CurrentHealth + health;
    }

    public void resetStats()
    {
        MaxHealth = baseMaxHealth;
    }
}
