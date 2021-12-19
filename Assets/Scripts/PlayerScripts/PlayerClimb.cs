using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles player climbing
public class PlayerClimb : MonoBehaviour
{
    public bool CanClimb = false;
    public float MovementSpeed = 15f;
    public float GravityScale;


    private CharacterMovementController Player;
    private PlayerJump playerJump;
    private Rigidbody2D rigidBody;
    private float movementAxis;
    private bool isClimbing = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerJump = gameObject.GetComponent<PlayerJump>();
        GravityScale = rigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate() 
    {   
        Climbing();
    }
    void Climbing(){
        // Climb only when not jumping
        if (CanClimb && !playerJump.HasJumped())
        {
            // While climing remove gravity and add velocity
            isClimbing = true;
            movementAxis = Input.GetAxis("Vertical");
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2( rigidBody.velocity.x, movementAxis*MovementSpeed);
        }
        else
        {
            rigidBody.gravityScale = GravityScale;
            if(isClimbing && !CanClimb)
            {
                isClimbing = false;
                movementAxis = 0;
                rigidBody.velocity = new Vector2( rigidBody.velocity.x, movementAxis*MovementSpeed);
            }
        }
    }
}
