using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script is used to handle movement for monsters
public class MonsterMovementScript : MonoBehaviour
{

    public float Orientation = 1;
    public float BaseMovementSpeed;
    public float MovementSpeed;
    public Transform Player;
    public float Distance = 5f;
    public Vector2 CurrentVelocity;
    public bool Hitting;
    public bool Knockback = false;

    protected Animator monsterAnimator;

    private float scaleX;
    private Rigidbody2D monsterRigidBody;
    private MonsterAI monsterBrain;
    private MonsterIdle monsterIde;



    // Base initialization
    void Start()
    {
        monsterBrain = gameObject.GetComponent<MonsterAI>();
        monsterRigidBody = gameObject.GetComponent<Rigidbody2D>();
        monsterAnimator = gameObject.GetComponent<Animator>();
        scaleX = gameObject.transform.localScale.x;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        monsterIde = gameObject.GetComponent<MonsterIdle>();
    }
    
    // Utility function to check if monster is hitting the ground
    public bool HittingGround(RaycastHit2D[] groundInfo)
    {
        foreach(RaycastHit2D hit in groundInfo)
        {
            if (hit.transform.tag.Equals("Ground"))
            {
                Hitting = true;
                return true;
            }
        }
        Hitting = false;
        return false;
    }

    // Check if monster can move to the position and set velocity accordingly
    void Update()
    {

        //float distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.position);
        if (monsterBrain.currentState.Equals(MonsterAI.MY_STATE.CLOSING_DISTANCE) && !Knockback)
        {

            RaycastHit2D[] groundInfo = Physics2D.RaycastAll(monsterBrain.enemyDetectionCircle.position, Vector2.down, Distance);

            // If hitting ground move
           if (HittingGround(groundInfo))
            {

                if(gameObject.transform.position.x != Player.position.x && Mathf.Abs((gameObject.transform.position -  Player.position).x) > 0.5)
                {
                    if (gameObject.transform.position.x < Player.position.x)
                    {
                        Orientation = 1;
                        CurrentVelocity = new Vector2(Orientation * MovementSpeed, monsterRigidBody.velocity.y);
                    }
                    else
                    {
                        Orientation = -1;
                        CurrentVelocity = new Vector2(Orientation * MovementSpeed, monsterRigidBody.velocity.y);
                    }
                }

            }
            else
           {
                //when not hitting ground, push back
                CurrentVelocity = new Vector2(Orientation * MovementSpeed * -1, monsterRigidBody.velocity.y);
               //currentVelocity = new Vector2(0, 0);
            }

        }
        else
        {
            if(monsterIde == null || !monsterIde.MyIdleMode.Equals(MonsterIdle.IDLE_MODE.WALKING) && !Knockback)
            {
                CurrentVelocity = new Vector2(0, monsterRigidBody.velocity.y);
            }
        }
        // Set rotation and animation
        gameObject.transform.localScale = new Vector2(Orientation * scaleX, gameObject.transform.localScale.y);
        monsterAnimator.SetFloat("movementSpeed", Mathf.Abs(CurrentVelocity.x));
    }

    void FixedUpdate()
    {
        //Debug.Log(currentVelocity);
        monsterRigidBody.velocity = CurrentVelocity;
    }
}
