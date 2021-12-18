using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles player jumps
public class PlayerJump : MonoBehaviour
{

    public float JumpForce = 10f;
    public int JumpNumber = 1;
    public float BaseJumpForce = 10f;
    public int BaseJumpNumber = 1;
    public float JumpCoolDown;
    public float CurrentJumpCoolDown;
    public JUMP_STATE JumpState = JUMP_STATE.READY;
    public bool Jumped = false;



    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;
    private PlayerDash playerDash;
    private int currJumpLeft = 1;


    // Base initialization
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerDash = gameObject.GetComponent<PlayerDash>();
    }


    void Update()
    {

        if(CurrentJumpCoolDown >= 0)
        {
            CurrentJumpCoolDown -= Time.deltaTime;
        }
        else
        {
            JumpState = JUMP_STATE.READY;
            // Reset jumps when not falling or jumping
            if(rigidBody.velocity.y == 0 && !playerDash.IsDashing())
            {
                ResetJumps();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && currJumpLeft > 0)
        {
            // Handle jump on space press
            if (JumpState.Equals(JUMP_STATE.READY))
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                currJumpLeft--;
                Jumped = true;
                CurrentJumpCoolDown = JumpCoolDown;
                Player.jumpCoolDown.setCooldown(JumpCoolDown);
                JumpState = JUMP_STATE.ON_COOLDOWN;
                Player.jumpNum.SetJumpNumber(currJumpLeft);
            }

        }
    }

    private void ResetJumps()
    {
        Jumped = false;
        currJumpLeft = JumpNumber;
        Player.jumpNum.SetJumpNumber(currJumpLeft);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Vector3 direction = transform.position - (col.gameObject.transform.position + col.gameObject.transform.localScale);
            if(direction.y > 0)
            {
                ResetJumps();
            }
        }
        
    }

    public bool HasJumped()
    {
        return Jumped;
    }

    public void SetJumpNumber(int jumpNumber)
    {

        //Debug.Log(HasJumped() + " " + currJumpLeft + " - " + jumpNumber);

        if (!Jumped)
        {
            currJumpLeft = jumpNumber;
        }

        this.JumpNumber = jumpNumber;
        Player.jumpNum.SetJumpNumber(currJumpLeft);
    }

    public enum JUMP_STATE
    {
        READY,
        ON_COOLDOWN
    }

    public void ResetStats()
    {
        JumpForce = BaseJumpForce;
        JumpNumber = BaseJumpNumber;
    }
}
