using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script is used to handle boss health.
// Function extends generic monster health that does all the basics
public class BossHealth : MonsterHealth
{

    private BossBrain bossBrain;
    
    // Base initialization
    protected override void Start()
    {
        base.Start();
        bossBrain = gameObject.GetComponent<BossBrain>();
        bossBrain.BossHealthBar.setMaxHealth(MaxHealth);
        bossBrain.BossHealthBar.setCurrentHealth(MaxHealth);
    }

    protected override void Update()
    {
        base.Update();
        // Boss is enraged when below half health
        if(CurrentHealth < (MaxHealth / 2))
        {
            bossBrain.isEnraged = true;
        }
    }

    public override void TakeDamage(float dmg)
    {
        // On hit, play animation and remove health
        if (CanHit)
        {
            monsterAnimator.SetTrigger("takeHit");
            CurrentHealth -= dmg;

            if (CurrentHealth <= 0)
            {
                monsterAnimator.SetBool("isDead", true);
            }
        }
        bossBrain.BossHealthBar.setCurrentHealth(CurrentHealth);
    }

    public override void HealObject(float health)
    {
        CurrentHealth = (CurrentHealth + health > MaxHealth) ? CurrentHealth : CurrentHealth + health;
        bossBrain.BossHealthBar.setCurrentHealth(CurrentHealth);
    }
}
