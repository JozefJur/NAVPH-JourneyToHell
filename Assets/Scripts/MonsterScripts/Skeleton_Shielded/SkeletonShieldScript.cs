using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skeleton shield handler
public class SkeletonShieldScript : MonoBehaviour
{


    public float BaseShieldCooldown;
    public float CurrentShieldCoolDown;
    public float ShieldDetectionRange;
    public Transform ShieldDetectionPoint;

    public SHIELD_STATE CurrentShieldState = SHIELD_STATE.READY;

    private SkeletonAI monsterBrain;
    private Animator monsterAnimator;

    // Base initialization
    void Start()
    {
        monsterBrain = gameObject.GetComponent<SkeletonAI>();
        monsterAnimator = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        if(CurrentShieldCoolDown >= 0)
        {
            CurrentShieldCoolDown -= Time.deltaTime;
        }
        else
        {
            if (CurrentShieldState.Equals(SHIELD_STATE.ON_COOLDOWN))
            {
                CurrentShieldState = SHIELD_STATE.READY;
            }
        }

        if (CanShield())
        {
            // Start shield animation and set state
            CurrentShieldState = SHIELD_STATE.SHIELDED;
            monsterBrain.IsShielded = true;
            monsterAnimator.SetBool("shield", true);
        }

    }

    // Check if player is close enough and cooldown is zero
    private bool CanShield()
    {
        if (!monsterBrain.IsShielded && CurrentShieldState.Equals(SHIELD_STATE.READY) && monsterBrain.currentState.Equals(MonsterAI.MY_STATE.CLOSING_DISTANCE))
        {
            return IsEnemyInShieldDistance();
        }
        return false;
    }


    // Check if player is close enough
    public bool IsEnemyInShieldDistance()
    {
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(ShieldDetectionPoint.position, ShieldDetectionRange, monsterBrain.enemyLayers);
        return enemyInRange != null && enemyInRange.Length > 0;
    }

    public void SetCoolDown()
    {
        monsterBrain.IsShielded = false;
        this.CurrentShieldCoolDown = BaseShieldCooldown;
        CurrentShieldState = SHIELD_STATE.ON_COOLDOWN;
    }

    public enum SHIELD_STATE
    {
        READY,
        ON_COOLDOWN,
        SHIELDED
    }
    
}
