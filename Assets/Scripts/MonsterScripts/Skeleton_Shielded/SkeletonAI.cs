using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skeleton AI implementation
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

        // Check if shielded
        if (IsShielded)
        {
            if (currentState.Equals(MY_STATE.IDLE) || !shieldScript.IsEnemyInShieldDistance())
            {
                IsShielded = false;
                animator.SetBool("shield", false);
                shieldScript.CurrentShieldState = SkeletonShieldScript.SHIELD_STATE.READY;
            }
            else
            {
                // When shielded, skeleton is 2 times slower
                movement.MovementSpeed = movement.BaseMovementSpeed / 2;
            }
            Shield.GetComponent<BoxCollider2D>().isTrigger = false;
            base.Update();
        }
        else
        {
            Shield.GetComponent<BoxCollider2D>().isTrigger = true;
            movement.MovementSpeed = movement.BaseMovementSpeed;
            base.Update();
        }
        
    }
}
