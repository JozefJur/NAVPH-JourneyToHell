using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles all monster melee attacks
public class MeleeMonsterAtackScript : MonoBehaviour
{
    public float BaseDmg = 10f;
    public float CurrentDmg = 10f;

    public float CurrentAttackSpeed = 1f;
    public float BaseAttackSpeed = 1f;

    public float LightAttackDuration = 0.5f;

    public float LightAttackCoolDown = 1;
    
    public AttackState CurrectAttackState = AttackState.READY;

    protected MonsterAI monsterBrain;
    protected Animator monsterAnimator;
    
    private float lightAttackC = 0f;
    private float lightAttackD = 0f;

    // Base initialization
    protected virtual void Start()
    {
        monsterBrain = gameObject.GetComponent<MonsterAI>();
        monsterAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckAttack();
    }

    private bool CanAttack()
    {
        return true;
    }

    // Function checks if monster can attack
    private void CheckAttack()
    {
        switch (CurrectAttackState)
        {
            case AttackState.READY:

                if (monsterBrain.currentState.Equals(MonsterAI.MY_STATE.ATTACKING) && CanAttack())
                {
                    // Set attack animation, and current state
                    monsterAnimator.SetTrigger("attack");
                    //monsterAnimator.SetLayerWeight(1, 1);
                    CurrectAttackState = AttackState.ATTACKING;
                    lightAttackD = LightAttackDuration;
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

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
                    lightAttackC = (LightAttackCoolDown - CurrentAttackSpeed);
                }
                break;
        }
    }

    // Function is called from animation
    // Function detects players in radius and deals damage
    void Attack()
    {
        Collider2D[] enemyToHit = Physics2D.OverlapCircleAll(monsterBrain.damageCircle.position, monsterBrain.damageRange, monsterBrain.enemyLayers);
        if (enemyToHit != null && enemyToHit.Length > 0)
        {
            enemyToHit[0].gameObject.GetComponent<PlayerHealth>().TakeDamage(CurrentDmg);
        }
    }

    public enum AttackState
    {
        READY,
        ATTACKING,
        ON_COOLDOWN
    }

}
