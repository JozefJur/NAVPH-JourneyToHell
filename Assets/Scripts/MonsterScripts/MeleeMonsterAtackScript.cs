using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterAtackScript : MonoBehaviour
{


    public float baseDmg = 10f;
    public float currentDmg = 10f;

    public float currentAttackSpeed = 1f;
    public float baseAttackSpeed = 1f;

    public float lightAttackDuration = 0.5f;

    public float lightAttackCoolDown = 1;
    
    private float lightAttackC = 0f;
    private float lightAttackD = 0f;

    public AttackState currectAttackState = AttackState.READY;
    protected MonsterAI monsterBrain;
    protected Animator monsterAnimator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        monsterBrain = gameObject.GetComponent<MonsterAI>();
        monsterAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
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

                if (monsterBrain.currentState.Equals(MonsterAI.MY_STATE.ATTACKING) && canAttack())
                {
                    monsterAnimator.SetTrigger("attack");
                    //monsterAnimator.SetLayerWeight(1, 1);
                    currectAttackState = AttackState.ATTACKING;
                    lightAttackD = lightAttackDuration;
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

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
        Collider2D[] enemyToHit = Physics2D.OverlapCircleAll(monsterBrain.damageCircle.position, monsterBrain.damageRange, monsterBrain.enemyLayers);
        if (enemyToHit != null && enemyToHit.Length > 0)
        {
            enemyToHit[0].gameObject.GetComponent<PlayerHealth>().takeDamage(currentDmg);
        }
    }

    public enum AttackState
    {
        READY,
        ATTACKING,
        ON_COOLDOWN
    }

}
