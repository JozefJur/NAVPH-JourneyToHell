using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 15f;
    public float baseMovementSpeed = 15f;
    public float sprintModifier = 25f;
    public MOVEMENT_STATE MovementState = MOVEMENT_STATE.WALKING;


    private float orientation = 1;
    private CharacterMovementController Player;
    private PlayerJump PlayerJump;
    private Rigidbody2D rigidBody;
    private float movementAxis;

    public enum MOVEMENT_STATE
    {
        WALKING,
        SPRINTING
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(movementAxis, 0, 0) * Time.deltaTime * (movementSpeed + (MovementState.Equals(MOVEMENT_STATE.SPRINTING) ? sprintModifier : 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        PlayerJump = gameObject.GetComponent<PlayerJump>();
    }

    // Update is called once per frame
    void Update()
    {
        movementAxis = Input.GetAxis("Horizontal");
        //transform.position += new Vector3(movementAxis, 0, 0) * Time.deltaTime * movementSpeed;

        checkSprint();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            orientation = Input.GetKeyDown(KeyCode.A) ? -1 : 1;
        }

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
