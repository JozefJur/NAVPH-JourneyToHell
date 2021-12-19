using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script controls all boss movement
public class BossMovement : MonoBehaviour
{
    public float Orientation = 1;
    public float MovementSpeed = 10;
    public Transform Player;
    public float Distance = 5f;
    public GameObject BossPlatform;
    public GameObject PlayerPlatform;
    public bool Hitting;

    public Vector2 TeleportCoords;
    public TELEPORTING_STAGE CurrentTeleportStage = TELEPORTING_STAGE.NOT_TELEPORTING;

    private BossBrain bossBrain;
    private Vector2 currentVelocity;
    private Rigidbody2D bossRigidBody;
    private float scaleX;
    private Animator bossAnimator;


    // Basic initialization
    void Start()
    {
        bossBrain = gameObject.GetComponent<BossBrain>();
        bossRigidBody = gameObject.GetComponent<Rigidbody2D>();
        bossAnimator = gameObject.GetComponent<Animator>();
        scaleX = gameObject.transform.localScale.x;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Unitility finction that checks if boss is hitting the ground
    private bool HittingGround(RaycastHit2D[] groundInfo)
    {
        foreach (RaycastHit2D hit in groundInfo)
        {
            if (hit.transform.tag.Equals("Ground"))
            {
                //Debug.Log("Boss " + hit.transform.gameObject.GetInstanceID());
                BossPlatform = hit.transform.gameObject;
                Hitting = true;
                return true;
            }
        }
        Hitting = false;
        return false;
    }

    // In update, movement is determined by boss stage and if player and boss is on the same platform
    void Update()
    {

        if (bossBrain.CurrentPhase.Equals(BossBrain.MY_PHASE.MELEE_PHASE))
        {
            if (bossBrain.CurrentStateMelee.Equals(BossBrain.MELEE_PHASE_STATE.CLOSING_DISTANCE_WALKING))
            {
                Walk();
            }
            else if (bossBrain.CurrentStateMelee.Equals(BossBrain.MELEE_PHASE_STATE.CLOSING_DISTANCE_TELEPORTING) && CurrentTeleportStage.Equals(TELEPORTING_STAGE.NOT_TELEPORTING))
            {
                CurrentTeleportStage = TELEPORTING_STAGE.TELEPORTING;
                Teleport();
            }
            else
            {
                currentVelocity = new Vector2(0, 0);
            }
        }

        gameObject.transform.localScale = new Vector2(Orientation * scaleX, gameObject.transform.localScale.y);
        bossAnimator.SetFloat("movementSpeed", Mathf.Abs(currentVelocity.x));
    }

    // Function handles boss movement towards player
    private void Walk()
    {
        if (gameObject.transform.position.x != Player.position.x && Mathf.Abs((gameObject.transform.position - Player.position).x) > 0.5)
        {

            RaycastHit2D[] groundInfo = Physics2D.RaycastAll(bossBrain.GroundChecker.position, Vector2.down, Distance);
            // Move only when on solid ground
            if (HittingGround(groundInfo))
            {

                // check orientation
                if (gameObject.transform.position.x < Player.position.x)
                {
                    Orientation = 1;
                    currentVelocity = new Vector2(Orientation * MovementSpeed, 0);
                }
                else
                {
                    Orientation = -1;
                    currentVelocity = new Vector2(Orientation * MovementSpeed, 0);
                }
            }
            else
            {
                currentVelocity = new Vector2(Orientation * MovementSpeed * -1, 0);
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


    // Function called from animation
    void PerformChange()
    {
        gameObject.transform.position = TeleportCoords;
        //bossBrain.currentState = BossBrain.MY_STATE.IDLE;
    }

    // Function called from animation
    // Function moves boss to the platform on which player currently stands
    void FinishTeleport()
    {
        bossBrain.CurrentStateMelee = BossBrain.MELEE_PHASE_STATE.IDLE;
        BossPlatform = PlayerPlatform;
        CurrentTeleportStage = TELEPORTING_STAGE.NOT_TELEPORTING;
        gameObject.GetComponent<MonsterHealth>().CanHit = true;
    }

    public void SetCurrentVelocity(Vector2 velocity)
    {
        this.currentVelocity = velocity;
    }

    void FixedUpdate()
    {
        bossRigidBody.velocity = currentVelocity;
    }
}
