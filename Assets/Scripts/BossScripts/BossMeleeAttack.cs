using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles melee attack of boss
public class BossMeleeAttack : MonoBehaviour
{
    public float BaseDmg = 40f;
    public float CurrentDmg = 40f;

    public float CurrentAttackSpeed = 0.5f;
    public float BaseAttackSpeed = 0.5f;

    public float LightAttackDuration = 1f;
    public AttackState CurrectAttackState = AttackState.READY;

    private float lightAttackCoolDown = 1f;

    private float lightAttackC = 0f;
    private float lightAttackD = 0f;

    private BossBrain bossBrain;
    private Animator bossAnimator;

    // Basic initialization
    void Start()
    {
        bossBrain = gameObject.GetComponent<BossBrain>();
        bossAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttack();
    }

    private bool CanAttack()
    {
        return true;
    }

    // Function checks state of the attack
    private void CheckAttack()
    {
        switch (CurrectAttackState)
        {
            case AttackState.READY:

                // When attack is ready and player is in range, play attack animation
                if (bossBrain.CurrentStateMelee.Equals(BossBrain.MELEE_PHASE_STATE.ATTACKING_MELEE) && CanAttack())
                {
                    
                    bossAnimator.SetTrigger("attack");
                    //monsterAnimator.SetLayerWeight(1, 1);
                    CurrectAttackState = AttackState.ATTACKING;
                    lightAttackD = LightAttackDuration;


                }

                break;
            case AttackState.ON_COOLDOWN:
                lightAttackC -= Time.deltaTime;
                if (lightAttackC <= 0)
                {
                    //monsterAnimator.SetLayerWeight(1, 0);
                    CurrectAttackState = AttackState.READY;
                }

                break;
            case AttackState.ATTACKING:
                lightAttackD -= Time.deltaTime;

                if (lightAttackD <= 0)
                {
                    CurrectAttackState = AttackState.ON_COOLDOWN;
                    lightAttackC = (lightAttackCoolDown - CurrentAttackSpeed);
                }
                break;
        }
    }

    //Function is called from animation
    void Attack()
    {
        // Get all game objects that collide with boss attack and apply damage
        Collider2D[] enemyToHit = Physics2D.OverlapCircleAll(bossBrain.DamageCircle.position, bossBrain.DamageRange, bossBrain.EnemyLayers);
        if (enemyToHit != null && enemyToHit.Length > 0)
        {
            enemyToHit[0].gameObject.GetComponent<PlayerHealth>().TakeDamage(BaseDmg);
        }
    }

    // Function is called from animation
    void FinishAttack()
    {
        bossBrain.CurrentStateMelee = BossBrain.MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING;
    }

    public enum AttackState
    {
        READY,
        ATTACKING,
        ON_COOLDOWN
    }
}
