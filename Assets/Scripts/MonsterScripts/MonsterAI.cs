using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{

    public Transform damageCircle;
    public float damageRange = 0.5f;
    public LayerMask enemyLayers;

    public Transform enemyDetectionCircle;
    public float detectionRange = 0.5f;

    public float aggroTime = 5f;
    private float aggroTimeLeft = 0;

    public MY_STATE currentState = MY_STATE.IDLE;

    private MeleeMonsterAtackScript attackScript;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        attackScript = gameObject.GetComponent<MeleeMonsterAtackScript>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log(attackScript.currectAttackState);
        if (!attackScript.currectAttackState.Equals(MeleeMonsterAtackScript.AttackState.ATTACKING)){

             detectEnemies();
        }
        detectAttack();
    }

    private void detectEnemies()
    {
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(enemyDetectionCircle.position, detectionRange, enemyLayers);
        if (enemyInRange != null && enemyInRange.Length > 0)
        {
            currentState = MY_STATE.CLOSING_DISTANCE;
            aggroTimeLeft = aggroTime;
        }
        else
        {
            aggroTimeLeft -= Time.deltaTime;
            if (aggroTimeLeft <= 0)
            {
                currentState = MY_STATE.IDLE;
            }
        }
    }

    private void detectAttack()
    {
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(damageCircle.position, damageRange, enemyLayers);
        if (enemyInRange != null && enemyInRange.Length > 0)
        {
            currentState = MY_STATE.ATTACKING;
            aggroTimeLeft = aggroTime;
        }
        else
        {
            aggroTimeLeft -= Time.deltaTime;
            if (aggroTimeLeft > 0 && !attackScript.currectAttackState.Equals(MeleeMonsterAtackScript.AttackState.ATTACKING))
            {
                currentState = MY_STATE.CLOSING_DISTANCE;
            }
        }
    }

    public enum MY_STATE
    {
        IDLE,
        CLOSING_DISTANCE,
        ATTACKING
    }

    void OnDrawGizmosSelected()
    {
        if (damageCircle != null)
        {
            Gizmos.DrawWireSphere(damageCircle.position, damageRange);
        }

        if(enemyDetectionCircle != null)
        {
            Gizmos.DrawWireSphere(enemyDetectionCircle.position, detectionRange);
        }

    }

}
