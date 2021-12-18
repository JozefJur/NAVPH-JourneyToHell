using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles player attack
public class PlayerAttack : MonoBehaviour
{

    public float BaseDmg = 40f;
    public float CurrentDmg = 40f;

    public float CurrentAttackSpeed = 1f;
    public float BaseAttackSpeed = 1f;

    public float LightAttackDuration = 0.5f;
    public float HeavyAttackDuration = 0.8f;

    public float LightAttackCoolDown = 1;
    public float HeavyAttackCoolDown = 3f;

    private AttackState LightAttackState = AttackState.READY;
    private AttackState HeavyAttackState = AttackState.READY;

    private float lightAttackC = 0f;
    private float heavyAttackC = 0f;

    private float lightAttackD = 0f;
    private float heavyAttackD = 0f;

    private Animator playerAnimator;
    private PlayerDash playerDash;
    private PlayerJump playerJump;
    private PlayerMovement playerMov;
    private Rigidbody2D rigidBody;
    private CharacterMovementController controller;


    public Transform sword;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Base initialization
    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerDash = gameObject.GetComponent<PlayerDash>();
        playerJump = gameObject.GetComponent<PlayerJump>();
        playerMov = gameObject.GetComponent<PlayerMovement>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        controller = gameObject.GetComponent<CharacterMovementController>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(lightAttackState + " " + heavyAttackState);
        playerAnimator.SetFloat("attackSpeed", CurrentAttackSpeed);
        lightAttack();
        heavyAttack();
    }

    // Check if player can attack
    // Player cant attack while jumping, falling, dashing or sprinting
    private bool canAttack()
    {
        return !playerDash.IsDashing() && !playerJump.HasJumped() && rigidBody.velocity.y == 0 && !playerMov.MovementState.Equals(PlayerMovement.MOVEMENT_STATE.SPRINTING);
    }
    //&& rigidBody.velocity.x == 0

    // Function returns closest enemy
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
        switch (LightAttackState)
        {
            case AttackState.READY:

                if (Input.GetMouseButtonDown(0) && canAttack())
                {
                    // Set attack animation
                    playerAnimator.SetTrigger("attack");
                    LightAttackState = AttackState.ATTACKING;
                    lightAttackD = LightAttackDuration;
                    controller.attackCoolDown.setCooldown(lightAttackD);

                }

                break;
            case AttackState.ON_COOLDOWN:
                lightAttackC -= Time.deltaTime;
                if (lightAttackC <= 0)
                {
                    LightAttackState = AttackState.READY;
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
                    LightAttackState = AttackState.ON_COOLDOWN;
                    lightAttackC = (LightAttackCoolDown - CurrentAttackSpeed);
                }
                break;
        }
    }

    // Function is called from Animation
    // Damage closest enemy
    void Attack()
    {
        Collider2D enemyToHit = findClosest(Physics2D.OverlapCircleAll(sword.position, attackRange, enemyLayers));
        if (enemyToHit != null)
        {
            enemyToHit.gameObject.GetComponent<MonsterHealth>().TakeDamage(CurrentDmg);
        }
    }

    // Function is called from Animation
    // Damage all enemies
    void HardAttackMultiple()
    {
        Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(sword.position, attackRange, enemyLayers);
        if (enemiesToHit != null && enemiesToHit.Length > 0)
        {
            foreach (var enemyToHit in enemiesToHit)
            {
                enemyToHit.gameObject.GetComponent<MonsterHealth>().TakeDamage(CurrentDmg);
            }
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
        switch (HeavyAttackState)
        {
            case AttackState.READY:

                if (Input.GetMouseButtonDown(1) && canAttack())
                {
                    // Set attack animation
                    playerAnimator.SetTrigger("hardAttack");
                    HeavyAttackState = AttackState.ATTACKING;
                    heavyAttackD = HeavyAttackDuration;
                    controller.heavyAttackCoolDown.setCooldown((HeavyAttackCoolDown - CurrentAttackSpeed) + HeavyAttackDuration);
                }

                break;
            case AttackState.ON_COOLDOWN:
                heavyAttackC -= Time.deltaTime;
                if (heavyAttackC <= 0)
                {
                    HeavyAttackState = AttackState.READY;
                }

                break;
            case AttackState.ATTACKING:
                heavyAttackD -= Time.deltaTime;
                if (heavyAttackD <= 0)
                {
                    HeavyAttackState = AttackState.ON_COOLDOWN;
                    heavyAttackC = HeavyAttackCoolDown - CurrentAttackSpeed;
                }
                break;
        }
    } 

    public bool IsLightAttacking()
    {
        return LightAttackState == AttackState.ATTACKING;
    }

    public bool IsHeavyAttacking()
    {
        return HeavyAttackState == AttackState.ATTACKING;
    }

    public bool IsAttacking()
    {
        return IsLightAttacking() || IsHeavyAttacking();
    }

    public void ResetStats()
    {
        CurrentDmg = BaseDmg;
        CurrentAttackSpeed = BaseAttackSpeed;
    }

    public enum AttackState
    {
        READY,
        ATTACKING,
        ON_COOLDOWN
    }
}
