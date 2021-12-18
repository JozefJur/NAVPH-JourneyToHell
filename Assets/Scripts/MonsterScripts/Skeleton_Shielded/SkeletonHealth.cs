using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Custom implementation of MonsterHealth for skeleton
public class SkeletonHealth : MonsterHealth
{

    private SkeletonShieldScript shieldScript;

    protected override void Start()
    {
        base.Start();
        shieldScript = gameObject.GetComponent<SkeletonShieldScript>();
    }

    public override void TakeDamage(float dmg)
    {

        // Absorb damage when sielded

        if (CanHit)
        {
            monsterAnimator.SetTrigger("takeHit");
            if (!shieldScript.CurrentShieldState.Equals(SkeletonShieldScript.SHIELD_STATE.SHIELDED))
            {
                CurrentHealth -= dmg;
            }
            else
            {
                monsterAnimator.SetBool("shield", false);
                shieldScript.SetCoolDown();

            }
            if (CurrentHealth <= 0)
            {
                monsterAnimator.SetBool("isDead", true);
            }

            MonsterHealthBar.setCurrentHealth(CurrentHealth);

        }

    }
}
