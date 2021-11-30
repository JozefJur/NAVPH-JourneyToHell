using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementScript : MonoBehaviour
{

    public float orientation = 1;
    public float movementSpeed;
    public Transform player;
    public float distance = 5f;

    private MonsterAI monsterBrain;
    private Vector2 currentVelocity;
    private Rigidbody2D monsterRigidBody;
    private float scaleX;
    private Animator monsterAnimator;

    public bool hitting;

    // Start is called before the first frame update
    void Start()
    {
        monsterBrain = gameObject.GetComponent<MonsterAI>();
        monsterRigidBody = gameObject.GetComponent<Rigidbody2D>();
        monsterAnimator = gameObject.GetComponent<Animator>();
        scaleX = gameObject.transform.localScale.x;
        player = player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private bool hittingGround(RaycastHit2D[] groundInfo)
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
        if (monsterBrain.currentState.Equals(MonsterAI.MY_STATE.CLOSING_DISTANCE))
        {

            RaycastHit2D[] groundInfo = Physics2D.RaycastAll(monsterBrain.enemyDetectionCircle.position, Vector2.down, distance);


           if (hittingGround(groundInfo))
            {

                if(gameObject.transform.position.x != player.position.x && Mathf.Abs((gameObject.transform.position -  player.position).x) > 0.5)
                {
                    if (gameObject.transform.position.x < player.position.x)
                    {
                        orientation = 1;
                        currentVelocity = new Vector2(orientation * movementSpeed, 0);
                    }
                    else
                    {
                        orientation = -1;
                        currentVelocity = new Vector2(orientation * movementSpeed, 0);
                    }
                }

            }
            else
           {
               currentVelocity = new Vector2(orientation * movementSpeed * -1, 0);
           }

        }
        else
        {
            currentVelocity = new Vector2(0, 0);
        }
        gameObject.transform.localScale = new Vector2(orientation * scaleX, gameObject.transform.localScale.y);
        monsterAnimator.SetFloat("movementSpeed", Mathf.Abs(currentVelocity.x));
    }

    void FixedUpdate()
    {
        monsterRigidBody.velocity = currentVelocity;
    }
}
