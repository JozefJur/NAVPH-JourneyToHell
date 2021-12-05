using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (canHit)
        {
            monsterAnimator.SetTrigger("takeHit");
            if (!shieldScript.currentShieldState.Equals(SkeletonShieldScript.SHIELD_STATE.SHIELDED))
            {
                CurrentHealth -= dmg;
            }
            else
            {
                monsterAnimator.SetBool("shield", false);
                shieldScript.setCoolDown();

            }
            if (CurrentHealth <= 0)
            {
                monsterAnimator.SetBool("isDead", true);
            }
        }

    }
}
