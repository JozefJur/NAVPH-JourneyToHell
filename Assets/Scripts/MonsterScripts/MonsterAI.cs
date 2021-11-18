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

    public MY_STATE currentState = MY_STATE.IDLE;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detectAttack();
        if (currentState.Equals(MY_STATE.IDLE)){

             detectEnemies();
        }
    }

    private void detectEnemies()
    {
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(enemyDetectionCircle.position, detectionRange, enemyLayers);
        if (enemyInRange != null && enemyInRange.Length > 0)
        {
            currentState = MY_STATE.CLOSING_DISTANCE;
        }
        else
        {
            currentState = MY_STATE.IDLE;
        }
    }

    private void detectAttack()
    {
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(damageCircle.position, damageRange, enemyLayers);
        if (enemyInRange != null && enemyInRange.Length > 0)
        {
            currentState = MY_STATE.ATTACKING;
        }
        else
        {
            currentState = MY_STATE.IDLE;
        }
    }

    public enum MY_STATE
    {
        IDLE,
        RUNNING_AWAY,
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
