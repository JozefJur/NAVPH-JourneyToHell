using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    public float baseDodgeForce = 60f;

    public float dodgeForce = 60f; // player dodge force
    public float dashDuration = 0.3f; // player dash duration

    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;
    private PlayerMovement PlayerMovement;
    public DashState dash = DashState.READY;
    private float dashCooldown = 0;
    private float currentDashTimer = 0;
    private Animator playerAnimator;

    private Vector2 appliedVelocity;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovement = gameObject.GetComponent<PlayerMovement>();
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isDashing())
        {
            rigidBody.velocity = appliedVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dash);
        switch (dash)
        {
            case DashState.READY:
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    playerAnimator.SetBool("IsDash", true);
                    dash = DashState.IN_PROGRESS;
                    //accVelocity = rigidBody.velocity;
                    rigidBody.velocity = new Vector2(0, 0);
                    appliedVelocity = new Vector2(PlayerMovement.getOrientation() * dodgeForce, rigidBody.velocity.y);
                    Player.dashCoolDown.setCooldown(dashDuration + 1f);
                }
                break;
            case DashState.IN_PROGRESS:
                currentDashTimer += Time.deltaTime;
                if (currentDashTimer >= dashDuration)
                {
                    playerAnimator.SetBool("IsDash", false);
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                    dash = DashState.ON_COOLDOWN;
                    dashCooldown = 1f;
                }
                break;
            case DashState.ON_COOLDOWN:
                dashCooldown -= Time.deltaTime;
                if (dashCooldown <= 0)
                {
                    dash = DashState.READY;
                    currentDashTimer = 0f;
                }
                break;
        }
    }


    public void resetStats()
    {
        dodgeForce = baseDodgeForce;
    }

    public enum DashState
    {
        READY,
        IN_PROGRESS,
        ON_COOLDOWN
    }

    public bool isDashing()
    {
        return dash.Equals(DashState.IN_PROGRESS);
    }

}
