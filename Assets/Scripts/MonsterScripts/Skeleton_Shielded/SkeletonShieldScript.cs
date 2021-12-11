using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShieldScript : MonoBehaviour
{

    private SkeletonAI monsterBrain;
    private Animator monsterAnimator;

    public float baseShieldCooldown;
    public float currentShieldCoolDown;
    public float shieldDetectionRange;
    public Transform shieldDetectionPoint;

    public SHIELD_STATE currentShieldState = SHIELD_STATE.READY;

    // Start is called before the first frame update
    void Start()
    {
        monsterBrain = gameObject.GetComponent<SkeletonAI>();
        monsterAnimator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(currentShieldCoolDown >= 0)
        {
            currentShieldCoolDown -= Time.deltaTime;
        }
        else
        {
            if (currentShieldState.Equals(SHIELD_STATE.ON_COOLDOWN))
            {
                currentShieldState = SHIELD_STATE.READY;
            }
        }

        if (canShield())
        {
            currentShieldState = SHIELD_STATE.SHIELDED;
            monsterBrain.IsShielded = true;
            monsterAnimator.SetBool("shield", true);
        }

    }

    private bool canShield()
    {
        if (!monsterBrain.IsShielded && currentShieldState.Equals(SHIELD_STATE.READY) && monsterBrain.currentState.Equals(MonsterAI.MY_STATE.CLOSING_DISTANCE))
        {
            return isEnemyInShieldDistance();
        }
        return false;
    }

    public bool isEnemyInShieldDistance()
    {
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(shieldDetectionPoint.position, shieldDetectionRange, monsterBrain.enemyLayers);
        return enemyInRange != null && enemyInRange.Length > 0;
    }

    public void setCoolDown()
    {
        monsterBrain.IsShielded = false;
        this.currentShieldCoolDown = baseShieldCooldown;
        currentShieldState = SHIELD_STATE.ON_COOLDOWN;
    }

    public enum SHIELD_STATE
    {
        READY,
        ON_COOLDOWN,
        SHIELDED
    }
    
}
