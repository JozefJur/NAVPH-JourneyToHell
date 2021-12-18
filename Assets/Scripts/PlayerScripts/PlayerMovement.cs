using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles player movement
public class PlayerMovement : MonoBehaviour
{

    public float MovementSpeed = 15f;
    public float BaseMovementSpeed = 15f;
    public float SprintModifier = 15f;
    public MOVEMENT_STATE MovementState = MOVEMENT_STATE.WALKING;
    public Transform GroundChecker;
    public GameObject PlatformHit;

    private float orientation = 1;
    private CharacterMovementController Player;
    private PlayerJump PlayerJump;
    private PlayerDash PlayerDash;
    private Rigidbody2D rigidBody;
    private float movementAxis;
    private Animator playerAnimator;
    private float scale;


    public enum MOVEMENT_STATE
    {
        WALKING,
        SPRINTING
    }

    // Apply velocity
    void FixedUpdate()
    {
        //   transform.position += new Vector3(movementAxis, 0, 0) * Time.deltaTime * (movementSpeed + (MovementState.Equals(MOVEMENT_STATE.SPRINTING) ? sprintModifier : 0));
        //  rigidBody.MovePosition(transform.position + new Vector3(movementAxis, rigidBody.velocity.y, 0) * Time.deltaTime * (movementSpeed + (MovementState.Equals(MOVEMENT_STATE.SPRINTING) ? sprintModifier : 0)));

        playerAnimator.SetFloat("speed", Mathf.Abs(movementAxis * MovementSpeed));
        playerAnimator.SetFloat("yVelocity", rigidBody.velocity.y);

        if (!PlayerDash.IsDashing())
        {
            rigidBody.velocity = new Vector2(movementAxis*(MovementSpeed + (MovementState.Equals(MOVEMENT_STATE.SPRINTING) ? SprintModifier : 0)), rigidBody.velocity.y);
        }
        //transform.Translate(new Vector3(movementAxis, 0, 0) * Time.deltaTime * (movementSpeed + (MovementState.Equals(MOVEMENT_STATE.SPRINTING) ? sprintModifier : 0)));
    }

    // Base initialization
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        PlayerJump = gameObject.GetComponent<PlayerJump>();
        PlayerDash = gameObject.GetComponent<PlayerDash>();
        playerAnimator = gameObject.GetComponent<Animator>();
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Get direction
        movementAxis = Input.GetAxis("Horizontal");


        RaycastHit2D[] groundInfo = Physics2D.RaycastAll(GroundChecker.position, Vector2.down, 1f);
        HittingGround(groundInfo);
        CheckSprint();

       // Get orientation
        if (movementAxis!=0f){
            orientation = movementAxis > 0 ? 1 : -1;
        }
        //Debug.Log(orientation);

        // Apply orientation
        transform.localScale = new Vector2(orientation * scale, transform.localScale.y);

    }

    // Check if player is hitting ground and get ground GameObject for boss
    private void HittingGround(RaycastHit2D[] groundInfo)
    {
        foreach (RaycastHit2D hit in groundInfo)
        {
            if (hit.transform.tag.Equals("Ground"))
            {
                //Debug.Log("Player " + hit.transform.gameObject.GetInstanceID());
                PlatformHit = hit.transform.gameObject;
                return;
            }
        }

        PlatformHit = null;

    }

    // Check if player is holding shift and set sprint state
    private void CheckSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && MovementState.Equals(MOVEMENT_STATE.WALKING) && (!PlayerJump.HasJumped()))
        {
            MovementState = MOVEMENT_STATE.SPRINTING;
        }
        else if (MovementState.Equals(MOVEMENT_STATE.SPRINTING) && Input.GetKeyUp(KeyCode.LeftShift))
        {
            MovementState = MOVEMENT_STATE.WALKING;
        }
    }

    public float GetOrientation()
    {
        return orientation;
    }

    public void ResetStats()
    {
        MovementSpeed = BaseMovementSpeed;
    }

}
