using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles player dash
public class PlayerDash : MonoBehaviour
{

    public float BaseDodgeForce = 60f;
    public float DodgeForce = 60f; // player dodge force
    public float DashDuration = 0.3f; // player dash duration
    public DashState Dash = DashState.READY;

    private PlayerMovement PlayerMovement;
    private float dashCooldown = 0;
    private float currentDashTimer = 0;
    private Animator playerAnimator;
    private PlayerHealth playerHealth;
    private Vector2 appliedVelocity;
    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;
    
    
    // Base initialization
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovement = gameObject.GetComponent<PlayerMovement>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        if (IsDashing())
        {
            rigidBody.velocity = appliedVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dash);
        switch (Dash)
        {
            case DashState.READY:
                if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && playerHealth.IsAlive())
                {
                    // While in dash, player can not be damaged, set dash animation, remove collision with skeleton layer
                    Physics2D.IgnoreLayerCollision(5, 7, true);
                    playerAnimator.SetBool("IsDash", true);
                    Dash = DashState.IN_PROGRESS;
                    //accVelocity = rigidBody.velocity;
                    rigidBody.velocity = new Vector2(0, 0);
                    appliedVelocity = new Vector2(PlayerMovement.GetOrientation() * DodgeForce, rigidBody.velocity.y);
                    Player.dashCoolDown.setCooldown(DashDuration + 1f);
                    playerHealth.CanHit = false;
                }
                break;
            case DashState.IN_PROGRESS:
                currentDashTimer += Time.deltaTime;
                if (currentDashTimer >= DashDuration)
                {
                    Physics2D.IgnoreLayerCollision(5, 7, false);
                    playerAnimator.SetBool("IsDash", false);
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                    Dash = DashState.ON_COOLDOWN;
                    dashCooldown = 1f;
                    playerHealth.CanHit = true;
                }
                break;
            case DashState.ON_COOLDOWN:
                dashCooldown -= Time.deltaTime;
                if (dashCooldown <= 0)
                {
                    Dash = DashState.READY;
                    currentDashTimer = 0f;
                }
                break;
        }
    }


    public void ResetStats()
    {
        DodgeForce = BaseDodgeForce;
    }

    public enum DashState
    {
        READY,
        IN_PROGRESS,
        ON_COOLDOWN
    }

    public bool IsDashing()
    {
        return Dash.Equals(DashState.IN_PROGRESS);
    }

}
