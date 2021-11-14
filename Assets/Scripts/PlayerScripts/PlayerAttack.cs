using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float baseDmg = 10f;
    public float currentDmg = 10f;

    public float currentAttackSpeed = 1f;
    public float baseAttackSpeed = 1f;

    public float lightAttackDuration = 0.5f;
    public float heavyAttackDuration = 3f;

    private float lightAttackCoolDown = 1;
    private float heavyAttackCoolDown = 5f;

    private AttackState lightAttackState = AttackState.READY;
    private AttackState heavyAttackState = AttackState.READY;

    private float lightAttackC = 0f;
    private float heavyAttackC = 0f;

    private float lightAttackD = 0f;
    private float heavyAttackD = 0f;

    private Animator playerAnimator;
    private PlayerDash playerDash;
    private PlayerJump playerJump;
    private PlayerMovement playerMov;
    private Rigidbody2D rigidBody;



    public Transform sword;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerDash = gameObject.GetComponent<PlayerDash>();
        playerJump = gameObject.GetComponent<PlayerJump>();
        playerMov = gameObject.GetComponent<PlayerMovement>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(lightAttackState + " " + heavyAttackState);
        playerAnimator.SetFloat("attackSpeed", currentAttackSpeed);
        lightAttack();
        heavyAttack();
    }

    private bool canAttack()
    {
        return !playerDash.isDashing() && !playerJump.HasJumped() && rigidBody.velocity.y == 0 && !playerMov.MovementState.Equals(PlayerMovement.MOVEMENT_STATE.SPRINTING);
    }
    //&& rigidBody.velocity.x == 0
    private Collider2D findClosest(Collider2D[] enemiesInRange)
    {
        if(enemiesInRange.Length == 0)
        {
            return null;
        }
        float lenght = Vector3.Distance(gameObject.transform.position, enemiesInRange[0].gameObject.transform.position);
        Collider2D enemy = enemiesInRange[0];
        foreach(Collider2D enemyCollider in enemiesInRange)
        {
            float enemyDistance = Vector3.Distance(gameObject.transform.position, enemyCollider.gameObject.transform.position);
            if(enemyDistance < lenght)
            {
                lenght = enemyDistance;
                enemy = enemyCollider;
            }
        }
        return enemy;
    }

    private void lightAttack()
    {
        switch (lightAttackState)
        {
            case AttackState.READY:

                if (Input.GetMouseButtonDown(0) && canAttack())
                {
                    playerAnimator.SetTrigger("attack");
                    lightAttackState = AttackState.ATTACKING;
                    lightAttackD = lightAttackDuration;


                }

                break;
            case AttackState.ON_COOLDOWN:
                lightAttackC -= Time.deltaTime;
                if (lightAttackC <= 0)
                {
                    lightAttackState = AttackState.READY;
                }

                break;
            case AttackState.ATTACKING:
                lightAttackD -= Time.deltaTime;

               /* if(lightAttackD <= lightAttackDuration / 2)
                {
                    Collider2D enemyToHit = findClosest(Physics2D.OverlapCircleAll(sword.position, attackRange, enemyLayers));
                    if (enemyToHit != null)
                    {
                        enemyToHit.gameObject.GetComponent<MonsterHealth>().TakeDamage(currentDmg);
                    }
                }*/

                if (lightAttackD <= 0)
                {
                    lightAttackState = AttackState.ON_COOLDOWN;
                    lightAttackC = (lightAttackCoolDown - currentAttackSpeed);
                }
                break;
        }
    }

    void Attack()
    {
        Collider2D enemyToHit = findClosest(Physics2D.OverlapCircleAll(sword.position, attackRange, enemyLayers));
        if (enemyToHit != null)
        {
            enemyToHit.gameObject.GetComponent<MonsterHealth>().TakeDamage(currentDmg);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(sword == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(sword.position, attackRange);
    }

    private void heavyAttack()
    {
        switch (heavyAttackState)
        {
            case AttackState.READY:

                if (Input.GetMouseButtonDown(1) && canAttack())
                {
                    heavyAttackState = AttackState.ATTACKING;
                    heavyAttackD = heavyAttackDuration;
                }

                break;
            case AttackState.ON_COOLDOWN:
                heavyAttackC -= Time.deltaTime;
                if (heavyAttackC <= 0)
                {
                    heavyAttackState = AttackState.READY;
                }

                break;
            case AttackState.ATTACKING:
                heavyAttackD -= Time.deltaTime;
                if (heavyAttackD <= 0)
                {
                    heavyAttackState = AttackState.ON_COOLDOWN;
                    heavyAttackC = lightAttackCoolDown;
                }
                break;
        }
    } 

    public bool isLightAttacking()
    {
        return lightAttackState == AttackState.ATTACKING;
    }

    public bool isHeavyAttacking()
    {
        return heavyAttackState == AttackState.ATTACKING;
    }

    public bool isAttacking()
    {
        return isLightAttacking() || isHeavyAttacking();
    }

    public void resetStats()
    {
        currentDmg = baseDmg;
        currentAttackSpeed = baseAttackSpeed;
    }

    public enum AttackState
    {
        READY,
        ATTACKING,
        ON_COOLDOWN
    }
}
