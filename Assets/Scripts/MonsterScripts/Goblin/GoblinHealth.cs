using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Custom implementation of MonsterHealth for goblin
public class GoblinHealth : MonsterHealth
{

    public float KnockBackPower;

    private GameObject player;
    private MonsterMovementScript movement;

    protected override void Start()
    {
        base.Start();
        movement = gameObject.GetComponent<MonsterMovementScript>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    public override void TakeDamage(float dmg)
    {

        base.TakeDamage(dmg);

        // Add knockback on hit
        if (CanHit && CurrentHealth > 0)
        {
            float orientation = player.GetComponent<PlayerMovement>().GetOrientation();
            Vector2 direction = new Vector2(orientation * 1, 1);
            movement.CurrentVelocity = direction * KnockBackPower;
            movement.Knockback = true;
            StartCoroutine(WaitknockBack());

        }

    }


    IEnumerator WaitknockBack()
    {
        yield return new WaitForSeconds(0.3f);
        movement.Knockback = false;
    }

}
