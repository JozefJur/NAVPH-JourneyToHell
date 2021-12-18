using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMeleeAttack : MeleeMonsterAtackScript
{

    protected SkeletonAI monsterBrainSkeleton;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        monsterBrainSkeleton = gameObject.GetComponent<SkeletonAI>();
    }


    protected override void Update()
    {
        if (!monsterBrainSkeleton.IsShielded)
        {
            base.Update();
        }
    }

    void Attack()
    {
        Collider2D[] enemyToHit = Physics2D.OverlapCircleAll(monsterBrain.damageCircle.position, monsterBrain.damageRange, monsterBrain.enemyLayers);
        if (enemyToHit != null && enemyToHit.Length > 0)
        {
            enemyToHit[0].gameObject.GetComponent<PlayerHealth>().TakeDamage(CurrentDmg);

        }
    }

}
