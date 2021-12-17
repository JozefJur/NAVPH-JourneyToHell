using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementScript : MonoBehaviour
{

    public float orientation = 1;
    public float baseMovementSpeed;
    public float movementSpeed;
    public Transform player;
    public float distance = 5f;

    private MonsterAI monsterBrain;
    public Vector2 currentVelocity;
    private Rigidbody2D monsterRigidBody;
    private float scaleX;
    protected Animator monsterAnimator;

    private MonsterIdle monsterIde;

    public bool hitting;

    public bool knockback = false;

    // Start is called before the first frame update
    void Start()
    {
        monsterBrain = gameObject.GetComponent<MonsterAI>();
        monsterRigidBody = gameObject.GetComponent<Rigidbody2D>();
        monsterAnimator = gameObject.GetComponent<Animator>();
        scaleX = gameObject.transform.localScale.x;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monsterIde = gameObject.GetComponent<MonsterIdle>();
    }
    
    public bool hittingGround(RaycastHit2D[] groundInfo)
    {
        foreach(RaycastHit2D hit in groundInfo)
        {
            if (hit.transform.tag.Equals("Ground"))
            {
                hitting = true;
                return true;
            }
        }
        hitting = false;
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        //float distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.position);
        if (monsterBrain.currentState.Equals(MonsterAI.MY_STATE.CLOSING_DISTANCE) && !knockback)
        {

            RaycastHit2D[] groundInfo = Physics2D.RaycastAll(monsterBrain.enemyDetectionCircle.position, Vector2.down, distance);


           if (hittingGround(groundInfo))
            {

                if(gameObject.transform.position.x != player.position.x && Mathf.Abs((gameObject.transform.position -  player.position).x) > 0.5)
                {
                    if (gameObject.transform.position.x < player.position.x)
                    {
                        orientation = 1;
                        currentVelocity = new Vector2(orientation * movementSpeed, monsterRigidBody.velocity.y);
                    }
                    else
                    {
                        orientation = -1;
                        currentVelocity = new Vector2(orientation * movementSpeed, monsterRigidBody.velocity.y);
                    }
                }

            }
            else
           {
                currentVelocity = new Vector2(orientation * movementSpeed * -1, monsterRigidBody.velocity.y);
               //currentVelocity = new Vector2(0, 0);
            }

        }
        else
        {
            if(monsterIde == null || !monsterIde.myIdleMode.Equals(MonsterIdle.IDLE_MODE.WALKING) && !knockback)
            {
                currentVelocity = new Vector2(0, monsterRigidBody.velocity.y);
            }
        }
        gameObject.transform.localScale = new Vector2(orientation * scaleX, gameObject.transform.localScale.y);
        monsterAnimator.SetFloat("movementSpeed", Mathf.Abs(currentVelocity.x));
    }

    void FixedUpdate()
    {
        //Debug.Log(currentVelocity);
        monsterRigidBody.velocity = currentVelocity;
    }
}
