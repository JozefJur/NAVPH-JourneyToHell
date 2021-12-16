using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrain : MonoBehaviour
{

    public Transform damageCircle;
    public float damageRange = 0.5f;
    public LayerMask enemyLayers;

    public Transform player;

    public Transform groundChecker;

    public MELEE_PHASE_STATE CurrentStateMelee = MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING;

    public TOTEM_PHASE_STATES CurrentStateTotem = TOTEM_PHASE_STATES.INITIALIZING;

    public MY_PHASE currentPhase = MY_PHASE.MELEE_PHASE;

    public float BaseSwitchTime = 5f;
    public float CurrentswitchTime = 5f;

    public HealthBarController bossHealthBar;

    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private BossMovement bossMovement;
    private MonsterHealth monsterHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();
        playerJump = player.GetComponent<PlayerJump>();
        bossMovement = gameObject.GetComponent<BossMovement>();
        monsterHealth = gameObject.GetComponent<MonsterHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchPhase();
        switch (currentPhase)
        {
            case MY_PHASE.MELEE_PHASE:
                MeleePhaseLogic();
                break;
            case MY_PHASE.TOTEM_PHASE:
                TotemPhaseLogic();
                break;
        }

   /*     if (!currentState.Equals(MY_STATE.CLOSING_DISTANCE_TELEPORTING))
        {
            monsterHealth.canHit = true;
            detectMeleeAttack();
        }

        if (currentPhase.Equals(MY_PHASE.MELEE_PHASE) && !currentState.Equals(MY_STATE.CLOSING_DISTANCE_TELEPORTING) && !currentState.Equals(MY_STATE.ATTACKING_MELEE))
        {
            monsterHealth.canHit = true;
            Debug.Log(playerJump.HasJumped());
            Debug.Log((playerMovement.platformHit != null && playerMovement.platformHit.GetInstanceID().Equals(bossMovement.BossPlatform.GetInstanceID())));
            if (playerJump.HasJumped() || (playerMovement.platformHit != null && playerMovement.platformHit.GetInstanceID().Equals(bossMovement.BossPlatform.GetInstanceID())))
            {
                CurrentStateMelee = MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING;
            }
            else
            {
                if(!playerJump.HasJumped() && (playerMovement.platformHit != null && !playerMovement.platformHit.GetInstanceID().Equals(bossMovement.BossPlatform.GetInstanceID())))
                {
                    CurrentStateMelee = MELEE_PHASE_STATE.CLOSING_DISTANCE_TELEPORTING;
                    bossMovement.teleportCoords = player.position;
                    bossMovement.teleportCoords.y += 7;
                    bossMovement.PlayerPlatform = playerMovement.platformHit;
                    bossMovement.currentTeleportStage = BossMovement.TELEPORTING_STAGE.NOT_TELEPORTING;
                    monsterHealth.canHit = false;
                }
            }
        }*/
    }


    private void MeleePhaseLogic()
    {
        if (!CurrentStateMelee.Equals(MELEE_PHASE_STATE.CLOSING_DISTANCE_TELEPORTING))
        {
            monsterHealth.canHit = true;
            detectMeleeAttack();
        }

        if (currentPhase.Equals(MY_PHASE.MELEE_PHASE) && !CurrentStateMelee.Equals(MELEE_PHASE_STATE.CLOSING_DISTANCE_TELEPORTING) && !CurrentStateMelee.Equals(MELEE_PHASE_STATE.ATTACKING_MELEE))
        {
            monsterHealth.canHit = true;
            //Debug.Log(playerJump.HasJumped());
            //Debug.Log((playerMovement.platformHit != null && playerMovement.platformHit.GetInstanceID().Equals(bossMovement.BossPlatform.GetInstanceID())));
            if (playerJump.HasJumped() || (playerMovement.platformHit != null && playerMovement.platformHit.GetInstanceID().Equals(bossMovement.BossPlatform.GetInstanceID())))
            {
                CurrentStateMelee = MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING;
            }
            else
            {
                if (!playerJump.HasJumped() && (playerMovement.platformHit != null && !playerMovement.platformHit.GetInstanceID().Equals(bossMovement.BossPlatform.GetInstanceID())))
                {
                    CurrentStateMelee = MELEE_PHASE_STATE.CLOSING_DISTANCE_TELEPORTING;
                    bossMovement.teleportCoords = player.position;
                    bossMovement.teleportCoords.y += 7;
                    bossMovement.PlayerPlatform = playerMovement.platformHit;
                    bossMovement.currentTeleportStage = BossMovement.TELEPORTING_STAGE.NOT_TELEPORTING;
                    monsterHealth.canHit = false;
                }
            }
        }
    }

    private void TotemPhaseLogic()
    {
        //GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("TotemSpawn");
        switch (CurrentStateTotem)
        {
            case TOTEM_PHASE_STATES.INITIALIZING:
                break;
            case TOTEM_PHASE_STATES.TELEPORTING:
                break;
            case TOTEM_PHASE_STATES.CASTING:
                break;
            case TOTEM_PHASE_STATES.EXITING:
                break;
        }

    }

    private void SwitchPhase()
    {
        CurrentswitchTime -= Time.deltaTime;
        if (CurrentswitchTime <= 0 && currentPhase.Equals(MY_PHASE.MELEE_PHASE))
        {
            currentPhase = MY_PHASE.TOTEM_PHASE;
            CurrentStateTotem = TOTEM_PHASE_STATES.INITIALIZING;
        }
    }

    private void detectMeleeAttack()
    {
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(damageCircle.position, damageRange, enemyLayers);
        if (enemyInRange != null && enemyInRange.Length > 0)
        {
            CurrentStateMelee = MELEE_PHASE_STATE.ATTACKING_MELEE;
        }
    }
    public enum MY_PHASE
    {
        MELEE_PHASE,
        TOTEM_PHASE,
        CASTING_PHASE
    }

    public enum MELEE_PHASE_STATE
    {
        IDLE,
        CLOSING_DISTANCE_WALKING,
        CLOSING_DISTANCE_TELEPORTING,
        ATTACKING_MELEE,
        CASTING,
    }

    public enum TOTEM_PHASE_STATES
    {
        INITIALIZING,
        TELEPORTING,
        CASTING,
        EXITING
    }

    void OnDrawGizmos()
    {
        if (damageCircle != null)
        {
            Gizmos.DrawWireSphere(damageCircle.position, damageRange);
        }

    }

}
