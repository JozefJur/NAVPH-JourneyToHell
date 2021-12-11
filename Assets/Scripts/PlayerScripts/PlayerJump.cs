using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    public float jumpForce = 10f;
    public int jumpNumber = 1;
    public float baseJumpForce = 10f;
    public int baseJumpNumber = 1;

    public float jumpCoolDown;
    public float currentJumpCoolDown;

    public JUMP_STATE jumpState = JUMP_STATE.READY;

    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;
    private PlayerDash playerDash;
    private int currJumpLeft = 1;

    public bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerDash = gameObject.GetComponent<PlayerDash>();
    }

    // Update is called once per frame
    void Update()
    {

        if(currentJumpCoolDown >= 0)
        {
            currentJumpCoolDown -= Time.deltaTime;
        }
        else
        {
            jumpState = JUMP_STATE.READY;
            if(rigidBody.velocity.y == 0 && !playerDash.isDashing())
            {
                resetJumps();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && currJumpLeft > 0)
        {

            if (jumpState.Equals(JUMP_STATE.READY))
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                currJumpLeft--;
                jumped = true;
                currentJumpCoolDown = jumpCoolDown;
                Player.jumpCoolDown.setCooldown(jumpCoolDown);
                jumpState = JUMP_STATE.ON_COOLDOWN;
                Player.jumpNum.SetJumpNumber(currJumpLeft);
            }

        }
    }

    private void resetJumps()
    {
        jumped = false;
        currJumpLeft = jumpNumber;
        Player.jumpNum.SetJumpNumber(currJumpLeft);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Collision" + " " + col.gameObject.tag + " "+ col.gameObject.name);
        if (col.gameObject.tag == "Ground")
        {
            //Debug.Log("Reset");
            Vector3 direction = transform.position - col.gameObject.transform.position;
            //Debug.Log(direction);
            if(direction.y > 0)
            {
                resetJumps();
                //Debug.Log("Here");
             //   jumped = false;
            //    currJumpLeft = jumpNumber;
             //   Player.jumpNum.SetJumpNumber(currJumpLeft);
                //Debug.Log(currJumpLeft);
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
        Player.jumpNum.SetJumpNumber(currJumpLeft);
    }

    public enum JUMP_STATE
    {
        READY,
        ON_COOLDOWN
    }

    public void resetStats()
    {
        jumpForce = baseJumpForce;
        jumpNumber = baseJumpNumber;
    }
}
