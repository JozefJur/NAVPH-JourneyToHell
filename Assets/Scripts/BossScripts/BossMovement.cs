using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float orientation = 1;
    public float movementSpeed;
    public Transform player;
    public float distance = 5f;
    public GameObject BossPlatform;
    public GameObject PlayerPlatform;

    public Vector2 teleportCoords;
    public TELEPORTING_STAGE currentTeleportStage = TELEPORTING_STAGE.NOT_TELEPORTING;

    private BossBrain bossBrain;
    private Vector2 currentVelocity;
    private Rigidbody2D bossRigidBody;
    private float scaleX;
    private Animator bossAnimator;



    public bool hitting;

    // Start is called before the first frame update
    void Start()
    {
        bossBrain = gameObject.GetComponent<BossBrain>();
        bossRigidBody = gameObject.GetComponent<Rigidbody2D>();
        bossAnimator = gameObject.GetComponent<Animator>();
        scaleX = gameObject.transform.localScale.x;
        player = player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private bool hittingGround(RaycastHit2D[] groundInfo)
    {
        foreach (RaycastHit2D hit in groundInfo)
        {
            if (hit.transform.tag.Equals("Ground"))
            {
                Debug.Log("Boss " + hit.transform.gameObject.GetInstanceID());
                BossPlatform = hit.transform.gameObject;
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

        if (bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.MELEE_PHASE))
        {
            if (bossBrain.CurrentStateMelee.Equals(BossBrain.MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING))
            {
                Walk();
            }
            else if (bossBrain.CurrentStateMelee.Equals(BossBrain.MELEE_PHASE_STATE.CLOSING_DISTANCE_TELEPORTING) && currentTeleportStage.Equals(TELEPORTING_STAGE.NOT_TELEPORTING))
            {
                currentTeleportStage = TELEPORTING_STAGE.TELEPORTING;
                Teleport();
            }
            else
            {
                currentVelocity = new Vector2(0, 0);
            }
        }



            //float distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.position);
      /*      if (bossBrain.currentState.Equals(MonsterAI.MY_STATE.CLOSING_DISTANCE))
        {

            RaycastHit2D[] groundInfo = Physics2D.RaycastAll(bossBrain.enemyDetectionCircle.position, Vector2.down, distance);


            if (hittingGround(groundInfo))
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
            else
            {
                currentVelocity = new Vector2(orientation * movementSpeed * -1, 0);
            }

        }
        else
        {
            currentVelocity = new Vector2(0, 0);
        }*/
        gameObject.transform.localScale = new Vector2(orientation * scaleX, gameObject.transform.localScale.y);
        bossAnimator.SetFloat("movementSpeed", Mathf.Abs(currentVelocity.x));
    }


    private void Walk()
    {
        if (gameObject.transform.position.x != player.position.x && Mathf.Abs((gameObject.transform.position - player.position).x) > 0.5)
        {

            RaycastHit2D[] groundInfo = Physics2D.RaycastAll(bossBrain.groundChecker.position, Vector2.down, distance);

            if (hittingGround(groundInfo))
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
            else
            {
                currentVelocity = new Vector2(orientation * movementSpeed * -1, 0);
            }
        }
    }


    public enum TELEPORTING_STAGE
    {
        NOT_TELEPORTING,
        TELEPORTING
    }

    private void Teleport()
    {
        bossAnimator.SetTrigger("teleport");
    }


    void PerformChange()
    {
        gameObject.transform.position = teleportCoords;
        //bossBrain.currentState = BossBrain.MY_STATE.IDLE;
    }

    void FinishTeleport()
    {
        bossBrain.CurrentStateMelee = BossBrain.MELEE_PHASE_STATE.IDLE;
        BossPlatform = PlayerPlatform;
        currentTeleportStage = TELEPORTING_STAGE.NOT_TELEPORTING;
        gameObject.GetComponent<MonsterHealth>().canHit = true;
    }

    public void setCurrentVelocity(Vector2 velocity)
    {
        this.currentVelocity = velocity;
    }

    void FixedUpdate()
    {
        bossRigidBody.velocity = currentVelocity;
    }
}
