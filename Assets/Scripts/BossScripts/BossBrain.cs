using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles all of the boss behavior
public class BossBrain : MonoBehaviour
{

    public Transform DamageCircle;
    public float DamageRange = 10f;
    public LayerMask EnemyLayers;
    public Transform Player;
    public Transform GroundChecker;
    public MELEE_PHASE_STATE CurrentStateMelee = MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING;
    public TOTEM_PHASE_STATES CurrentStateTotem = TOTEM_PHASE_STATES.INITIALIZING;
    public MY_PHASE CurrentPhase = MY_PHASE.MELEE_PHASE;
    public float BaseSwitchTime = 20f;
    public float CurrentswitchTime = 20f;
    public bool isEnraged = false;
    public HealthBarController BossHealthBar;

    
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private BossMovement bossMovement;
    private MonsterHealth monsterHealth;

    // Base initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = Player.GetComponent<PlayerMovement>();
        playerJump = Player.GetComponent<PlayerJump>();
        bossMovement = gameObject.GetComponent<BossMovement>();
        monsterHealth = gameObject.GetComponent<MonsterHealth>();
    }

    // In update we check what is the current phase, and if we want to switch
    void Update()
    {
        SwitchPhase();
        switch (CurrentPhase)
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


    // Function handles all the logic regarding melee boss phase
    private void MeleePhaseLogic()
    {
        if (!CurrentStateMelee.Equals(MELEE_PHASE_STATE.CLOSING_DISTANCE_TELEPORTING))
        {
            // When not teleporting, check if player can be attacked
            monsterHealth.CanHit = true;
            DetectMeleeAttack();
        }

        if (CurrentPhase.Equals(MY_PHASE.MELEE_PHASE) && !CurrentStateMelee.Equals(MELEE_PHASE_STATE.CLOSING_DISTANCE_TELEPORTING) && !CurrentStateMelee.Equals(MELEE_PHASE_STATE.ATTACKING_MELEE))
        {
            monsterHealth.CanHit = true;
            //Debug.Log(playerJump.HasJumped());
            //Debug.Log((playerMovement.platformHit != null && playerMovement.platformHit.GetInstanceID().Equals(bossMovement.BossPlatform.GetInstanceID())));
            if (playerJump.HasJumped() || (playerMovement.PlatformHit != null && playerMovement.PlatformHit.GetInstanceID().Equals(bossMovement.BossPlatform.GetInstanceID())))
            {

                // Close the distance by walking when on the same platform
                
                CurrentStateMelee = MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING;
            }
            else
            {
                if (!playerJump.HasJumped() && (playerMovement.PlatformHit != null && !playerMovement.PlatformHit.GetInstanceID().Equals(bossMovement.BossPlatform.GetInstanceID())))
                {
                    // Teleport to player when on different platform

                    CurrentStateMelee = MELEE_PHASE_STATE.CLOSING_DISTANCE_TELEPORTING;
                    bossMovement.TeleportCoords = Player.position;
                    bossMovement.TeleportCoords.y += 7;
                    bossMovement.PlayerPlatform = playerMovement.PlatformHit;
                    bossMovement.CurrentTeleportStage = BossMovement.TELEPORTING_STAGE.NOT_TELEPORTING;
                    monsterHealth.CanHit = false;
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

    // Function is used to switch boss phase when possible
    private void SwitchPhase()
    {
        CurrentswitchTime -= Time.deltaTime;
        if (CurrentswitchTime <= 0 && CurrentPhase.Equals(MY_PHASE.MELEE_PHASE))
        {
            CurrentPhase = MY_PHASE.TOTEM_PHASE;
            CurrentStateTotem = TOTEM_PHASE_STATES.INITIALIZING;
            CurrentStateMelee = MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING;
        }
    }

    //Function checks if player is in range of boss attack
    private void DetectMeleeAttack()
    {
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(DamageCircle.position, DamageRange, EnemyLayers);
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
        if (DamageCircle != null)
        {
            Gizmos.DrawWireSphere(DamageCircle.position, DamageRange);
        }

    }

}
