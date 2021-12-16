using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonsterHealth
{

    private BossBrain bossBrain;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bossBrain = gameObject.GetComponent<BossBrain>();
        bossBrain.bossHealthBar.setMaxHealth(MaxHealth);
        bossBrain.bossHealthBar.setCurrentHealth(MaxHealth);
    }

    public override void TakeDamage(float dmg)
    {

        if (canHit)
        {
            monsterAnimator.SetTrigger("takeHit");
            CurrentHealth -= dmg;

            if (CurrentHealth <= 0)
            {
                monsterAnimator.SetBool("isDead", true);
            }
        }
        bossBrain.bossHealthBar.setCurrentHealth(CurrentHealth);
    }

    public override void HealObject(float health)
    {
        CurrentHealth = (CurrentHealth + health > MaxHealth) ? CurrentHealth : CurrentHealth + health;
        bossBrain.bossHealthBar.setCurrentHealth(CurrentHealth);
    }
}
