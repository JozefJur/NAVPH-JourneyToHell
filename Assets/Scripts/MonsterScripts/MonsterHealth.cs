using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles monster health and death
public class MonsterHealth : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth = 100;
    public bool CanHit = true;
    public HealthBarController MonsterHealthBar;

    private Rigidbody2D RigidBody;
    protected Animator monsterAnimator;
    private ItemGiver itemGiver;

    // Base initialization
    protected virtual void Start()
    {
        RigidBody = gameObject.GetComponent<Rigidbody2D>();
        monsterAnimator = gameObject.GetComponent<Animator>();
        itemGiver = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ItemGiver>();
        MonsterHealthBar.setMaxHealth(MaxHealth);
        MonsterHealthBar.setCurrentHealth(MaxHealth);
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       /* if(CurrentHealth <= 0)
        {
            Debug.Log("DEAD MONSTER");

            //monsterAnimator.SetBool("isDead");

            Destroy(gameObject);
            //TODO IMPLEMENT DEATH !!! (mozeme pridat item, ktory napr zabrani smrti a zmizne)
        }
       */

        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    // Function is used to remove monster health and to set death animation
    public virtual void TakeDamage(float dmg)
    {

        if (CanHit)
        {
            monsterAnimator.SetTrigger("takeHit");
            CurrentHealth -= dmg;

            if (CurrentHealth <= 0)
            {
                PlayerStatistics.NumberOfKilledMonsters += 1;
                monsterAnimator.SetBool("isDead", true);
            }
        }
        MonsterHealthBar.setCurrentHealth(CurrentHealth);
    }

    // Function is called from animation
    // Function destroys GameObject and drops item
    void DeleteMonster()
    {
        GameObject item = itemGiver.GetRandomItem();
        Vector3 oldPosition = gameObject.transform.position;
        GameObject.FindGameObjectWithTag("Director").GetComponent<Narrator>().RemoveMonster(gameObject);
        Destroy(gameObject);
        if(item != null)
        {
            Instantiate(item, oldPosition, Quaternion.identity);
        }
    }

    public virtual void HealObject(float health)
    {
        CurrentHealth = (CurrentHealth + health > MaxHealth) ? CurrentHealth : CurrentHealth + health;
        MonsterHealthBar.setCurrentHealth(CurrentHealth);
    }

}
