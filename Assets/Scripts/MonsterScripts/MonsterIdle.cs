using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles monster idle movement
public class MonsterIdle : MonoBehaviour
{
    // Start is called before the first frame update

    public float WalkingTime;
    public float DirectionChangeTime;
    public float WalkingTimeout;
    public IDLE_MODE MyIdleMode = IDLE_MODE.STANDING;


    private float currentWalkingTimeout;
    private float currentWalkingTime;
    private MonsterAI monsterBrain;
    private MonsterMovementScript monsterMovement;

    // Base initialization
    void Start()
    {
        monsterBrain = gameObject.GetComponent<MonsterAI>();
        monsterMovement = gameObject.GetComponent<MonsterMovementScript>();
    }

    // Update handles idle movement
    void Update()
    {
       if (monsterBrain.currentState.Equals(MonsterAI.MY_STATE.IDLE))
        {
            switch (MyIdleMode)
            {
                case IDLE_MODE.STANDING:
                    SimulateStanding();
                    break;
                case IDLE_MODE.WALKING:
                    SimulateWalking();
                    break;
            }
        }
        else
        {
            MyIdleMode = IDLE_MODE.STANDING;
        }
    }

    // Function tries to generate random movement patters for monster
    // Monster walks for random amount time and then changes direction
    // Direction is also changed, when monster hits a wall
    private void SimulateWalking()
    {
        currentWalkingTime -= Time.deltaTime;
        DirectionChangeTime -= Time.deltaTime;
        if (currentWalkingTime <= 0)
        {
            MyIdleMode = IDLE_MODE.STANDING;
            currentWalkingTimeout = WalkingTimeout;
        }

        RaycastHit2D[] groundInfo = Physics2D.RaycastAll(monsterBrain.enemyDetectionCircle.position, Vector2.down, monsterMovement.Distance);
        // When not hitting ground or hitting wall, change orientation
        if (!monsterMovement.HittingGround(groundInfo) || HittingWall())
        {
            monsterMovement.Orientation *= -1;
            DirectionChangeTime = Random.Range(3, 8);
        }

        if (DirectionChangeTime <= 0)
        {
            monsterMovement.Orientation *= -1;
            DirectionChangeTime = Random.Range(3, 8);
        }

        monsterMovement.CurrentVelocity = new Vector2(monsterMovement.Orientation * monsterMovement.MovementSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

    }

    // Utility function to check if monster is hitting a wall
    public bool HittingWall()
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

    private void SimulateStanding()
    {
        currentWalkingTimeout -= Time.deltaTime;
        if(currentWalkingTimeout <= 0)
        {
            MyIdleMode = IDLE_MODE.WALKING;
            currentWalkingTime = WalkingTime;
        }
    }


    public enum IDLE_MODE
    {
        STANDING,
        WALKING
    }

}
