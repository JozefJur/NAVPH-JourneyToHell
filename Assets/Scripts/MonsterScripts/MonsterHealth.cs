using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth = 100;
    
    private Rigidbody2D RigidBody;
    private Animator monsterAnimator;

    private ItemGiver itemGiver;

    // Start is called before the first frame update
    void Start()
    {
        RigidBody = gameObject.GetComponent<Rigidbody2D>();
        monsterAnimator = gameObject.GetComponent<Animator>();
        itemGiver = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ItemGiver>();
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
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

    public void TakeDamage(float dmg)
    {
        monsterAnimator.SetTrigger("takeHit");
        CurrentHealth -= dmg;

        if (CurrentHealth <= 0)
        {
            monsterAnimator.SetBool("isDead", true);
        }
    }

    void DeleteMonster()
    {
        GameObject item = itemGiver.getRandomItem();
        Vector3 oldPosition = gameObject.transform.position;
        Destroy(gameObject);
        if(item != null)
        {
            Instantiate(item, oldPosition, Quaternion.identity);
        }
    }

    public void HealObject(float health)
    {
        CurrentHealth = (CurrentHealth + health > MaxHealth) ? CurrentHealth : CurrentHealth + health;
    }

}
