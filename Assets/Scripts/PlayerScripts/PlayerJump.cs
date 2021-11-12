using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    public float jumpForce = 10f;
    public int jumpNumber = 1;
    public float baseJumpForce = 10f;
    public int baseJumpNumber = 1;

    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;
    private int currJumpLeft = 1;

    private bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currJumpLeft > 0)
        {

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            currJumpLeft--;
            jumped = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            Vector3 direction = transform.position - col.gameObject.transform.position;
            if(direction.y > 0)
            {
                jumped = false;
                currJumpLeft = jumpNumber;
            }
        }
        
    }

    public bool HasJumped()
    {
        return jumped;
    }

    public void SetJumpNumber(int jumpNumber)
    {

        //Debug.Log(HasJumped() + " " + currJumpLeft + " - " + jumpNumber);

        if (!jumped)
        {
            currJumpLeft = jumpNumber;
        }

        this.jumpNumber = jumpNumber;
    }

    public void resetStats()
    {
        jumpForce = baseJumpForce;
        jumpNumber = baseJumpNumber;
    }
}
