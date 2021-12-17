using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonoBehaviour
{
    // Start is called before the first frame update

    private MonsterAI monsterBrain;
    private MonsterMovementScript monsterMovement;
    public float walkingTime;
    private float currentWalkingTime;
    public float walkingTimeout;
    private float currentWalkingTimeout;

    public IDLE_MODE myIdleMode = IDLE_MODE.STANDING;

    public float directionChangeTime;
    void Start()
    {
        monsterBrain = gameObject.GetComponent<MonsterAI>();
        monsterMovement = gameObject.GetComponent<MonsterMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
       if (monsterBrain.currentState.Equals(MonsterAI.MY_STATE.IDLE))
        {
            switch (myIdleMode)
            {
                case IDLE_MODE.STANDING:
                    simulateStanding();
                    break;
                case IDLE_MODE.WALKING:
                    simulateWalking();
                    break;
            }
        }
        else
        {
            myIdleMode = IDLE_MODE.STANDING;
        }
    }

    private void simulateWalking()
    {
        currentWalkingTime -= Time.deltaTime;
        directionChangeTime -= Time.deltaTime;
        if (currentWalkingTime <= 0)
        {
            myIdleMode = IDLE_MODE.STANDING;
            currentWalkingTimeout = walkingTimeout;
        }

        RaycastHit2D[] groundInfo = Physics2D.RaycastAll(monsterBrain.enemyDetectionCircle.position, Vector2.down, monsterMovement.distance);
        if (!monsterMovement.hittingGround(groundInfo) || hittingWall())
        {
            monsterMovement.orientation *= -1;
            directionChangeTime = Random.Range(3, 8);
        }

        if (directionChangeTime <= 0)
        {
            monsterMovement.orientation *= -1;
            directionChangeTime = Random.Range(3, 8);
        }

        monsterMovement.currentVelocity = new Vector2(monsterMovement.orientation * monsterMovement.movementSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

    }

    public bool hittingWall()
    {
        RaycastHit2D[] groundInfo = Physics2D.RaycastAll(monsterBrain.enemyDetectionCircle.position, Vector2.right, 0.1f);
        foreach (RaycastHit2D hit in groundInfo)
        {
            if (hit.transform.tag.Equals("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    private void simulateStanding()
    {
        currentWalkingTimeout -= Time.deltaTime;
        if(currentWalkingTimeout <= 0)
        {
            myIdleMode = IDLE_MODE.WALKING;
            currentWalkingTime = walkingTime;
        }
    }


    public enum IDLE_MODE
    {
        STANDING,
        WALKING
    }

}
