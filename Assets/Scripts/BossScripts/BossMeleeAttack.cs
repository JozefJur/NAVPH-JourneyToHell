using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    public float baseDmg = 40f;
    public float currentDmg = 40f;

    public float currentAttackSpeed = 1f;
    public float baseAttackSpeed = 1f;

    public float lightAttackDuration = 0.5f;

    private float lightAttackCoolDown = 1;

    private float lightAttackC = 0f;
    private float lightAttackD = 0f;

    public AttackState currectAttackState = AttackState.READY;
    private BossBrain bossBrain;
    private Animator bossAnimator;

    // Start is called before the first frame update
    void Start()
    {
        bossBrain = gameObject.GetComponent<BossBrain>();
        bossAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkAttack();
    }

    private bool canAttack()
    {
        return true;
    }

    private void checkAttack()
    {
        switch (currectAttackState)
        {
            case AttackState.READY:

                if (bossBrain.CurrentStateMelee.Equals(BossBrain.MELEE_PHASE_STATE.ATTACKING_MELEE) && canAttack())
                {
                    bossAnimator.SetTrigger("attack");
                    //monsterAnimator.SetLayerWeight(1, 1);
                    currectAttackState = AttackState.ATTACKING;
                    lightAttackD = lightAttackDuration;


                }

                break;
            case AttackState.ON_COOLDOWN:
                lightAttackC -= Time.deltaTime;
                if (lightAttackC <= 0)
                {
                    //monsterAnimator.SetLayerWeight(1, 0);
                    currectAttackState = AttackState.READY;
                }

                break;
            case AttackState.ATTACKING:
                lightAttackD -= Time.deltaTime;

                if (lightAttackD <= 0)
                {
                    currectAttackState = AttackState.ON_COOLDOWN;
                    lightAttackC = (lightAttackCoolDown - currentAttackSpeed);
                }
                break;
        }
    }

    void Attack()
    {
        Debug.Log("HERE AT");
        Collider2D[] enemyToHit = Physics2D.OverlapCircleAll(bossBrain.damageCircle.position, bossBrain.damageRange, bossBrain.enemyLayers);
        if (enemyToHit != null && enemyToHit.Length > 0)
        {
            enemyToHit[0].gameObject.GetComponent<PlayerHealth>().takeDamage(baseDmg);
        }
    }

    void FinishAttack()
    {
        Debug.Log("HERE AT FIN");
        bossBrain.CurrentStateMelee = BossBrain.MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING;
    }

    public enum AttackState
    {
        READY,
        ATTACKING,
        ON_COOLDOWN
    }
}
