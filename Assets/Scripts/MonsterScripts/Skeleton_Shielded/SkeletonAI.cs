using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonsterAI
{

    public bool IsShielded;
    public GameObject Shield;
    private MonsterMovementScript movement;
    private SkeletonShieldScript shieldScript;

    protected override void Start()
    {
        base.Start();
        movement = gameObject.GetComponent<MonsterMovementScript>();
        shieldScript = gameObject.GetComponent<SkeletonShieldScript>();
    }

    // Update is called once per frame
    protected override void Update()
    {

        if (IsShielded)
        {
            if (currentState.Equals(MY_STATE.IDLE) || !shieldScript.isEnemyInShieldDistance())
            {
                IsShielded = false;
                animator.SetBool("shield", false);
                shieldScript.currentShieldState = SkeletonShieldScript.SHIELD_STATE.READY;
            }
            else
            {
                movement.movementSpeed = movement.baseMovementSpeed / 2;
            }
            Shield.GetComponent<BoxCollider2D>().isTrigger = false;
            base.Update();
        }
        else
        {
            Shield.GetComponent<BoxCollider2D>().isTrigger = true;
            movement.movementSpeed = movement.baseMovementSpeed;
            base.Update();
        }
        
    }
}
