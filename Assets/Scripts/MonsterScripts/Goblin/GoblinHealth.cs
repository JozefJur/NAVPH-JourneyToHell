using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (canHit && CurrentHealth > 0)
        {
            float orientation = player.GetComponent<PlayerMovement>().getOrientation();
            Vector2 direction = new Vector2(orientation * 1, 1);
            movement.currentVelocity = direction * KnockBackPower;
            movement.knockback = true;
            StartCoroutine(waitknockBack());

        }

    }


    IEnumerator waitknockBack()
    {
        yield return new WaitForSeconds(0.3f);
        movement.knockback = false;
    }

}
