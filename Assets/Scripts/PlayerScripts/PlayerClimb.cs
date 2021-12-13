using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public bool canClimb = false;
    public float movementSpeed = 15f;

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
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate() 
    {   
        climbing();
    }
    void climbing(){
        if (canClimb && !playerJump.HasJumped())
        {
            isClimbing = true;
            movementAxis = Input.GetAxis("Vertical");
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2( rigidBody.velocity.x, movementAxis*movementSpeed);
          /*  if(Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Up");
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Down");
            }*/
        }
        else
        {
            rigidBody.gravityScale = 1;
            if(isClimbing && !canClimb)
            {
                isClimbing = false;
                movementAxis = 0;
                rigidBody.velocity = new Vector2( rigidBody.velocity.x, movementAxis*movementSpeed);
            }
        }
    }
}
