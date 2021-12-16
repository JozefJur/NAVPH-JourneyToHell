using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 15f;
    public float baseMovementSpeed = 15f;
    public float sprintModifier = 15f;
    public MOVEMENT_STATE MovementState = MOVEMENT_STATE.WALKING;
    public Transform GroundChecker;
    public GameObject platformHit;

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

    void FixedUpdate()
    {
        //   transform.position += new Vector3(movementAxis, 0, 0) * Time.deltaTime * (movementSpeed + (MovementState.Equals(MOVEMENT_STATE.SPRINTING) ? sprintModifier : 0));
        //  rigidBody.MovePosition(transform.position + new Vector3(movementAxis, rigidBody.velocity.y, 0) * Time.deltaTime * (movementSpeed + (MovementState.Equals(MOVEMENT_STATE.SPRINTING) ? sprintModifier : 0)));

        /*
         *   FIXNUT NARAZ Z BOKU A DODGE
        */
        playerAnimator.SetFloat("speed", Mathf.Abs(movementAxis * movementSpeed));
        playerAnimator.SetFloat("yVelocity", rigidBody.velocity.y);

        if (!PlayerDash.isDashing())
        {
            rigidBody.velocity = new Vector2(movementAxis*(movementSpeed + (MovementState.Equals(MOVEMENT_STATE.SPRINTING) ? sprintModifier : 0)), rigidBody.velocity.y);
        }
        //transform.Translate(new Vector3(movementAxis, 0, 0) * Time.deltaTime * (movementSpeed + (MovementState.Equals(MOVEMENT_STATE.SPRINTING) ? sprintModifier : 0)));
    }

    // Start is called before the first frame update
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
        movementAxis = Input.GetAxis("Horizontal");
        // playerAnimator.SetFloat("speed", Mathf.Abs(movementAxis * movementSpeed));
        // playerAnimator.SetFloat("yVelocity", rigidBody.velocity.y);
        //transform.position += new Vector3(movementAxis, 0, 0) * Time.deltaTime * movementSpeed;

        RaycastHit2D[] groundInfo = Physics2D.RaycastAll(GroundChecker.position, Vector2.down, 1f);
        hittingGround(groundInfo);
        checkSprint();

       /* if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            orientation = Input.GetKeyDown(KeyCode.A) ? -1 : 1;
        }*/

        orientation = movementAxis > 0 ? 1 : -1;

        //Debug.Log(orientation);

        transform.localScale = new Vector2(orientation * scale, transform.localScale.y);

    }

    private void hittingGround(RaycastHit2D[] groundInfo)
    {
        foreach (RaycastHit2D hit in groundInfo)
        {
            if (hit.transform.tag.Equals("Ground"))
            {
                //Debug.Log("Player " + hit.transform.gameObject.GetInstanceID());
                platformHit = hit.transform.gameObject;
                return;
            }
        }

        platformHit = null;

    }

    private void checkSprint()
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

    public float getOrientation()
    {
        return orientation;
    }

    public void resetStats()
    {
        movementSpeed = baseMovementSpeed;
    }

}
