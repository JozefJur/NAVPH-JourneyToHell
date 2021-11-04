using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    public float baseDodgeForce = 20f;

    public float dodgeForce = 20f; // player dodge force
    public float dashDuration = 0.3f; // player dash duration

    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;
    private PlayerMovement PlayerMovement;
    private DashState dash = DashState.READY;
    private float dashCooldown = 0;
    private float currentDashTimer = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (dash)
        {
            case DashState.READY:
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    dash = DashState.IN_PROGRESS;
                    //accVelocity = rigidBody.velocity;
                    rigidBody.velocity = new Vector2(PlayerMovement.getOrientation() * dodgeForce, rigidBody.velocity.y);
                }
                break;
            case DashState.IN_PROGRESS:
                currentDashTimer += Time.deltaTime;
                if (currentDashTimer >= dashDuration)
                {
                    rigidBody.velocity = new Vector2(0, 0); ;
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

}
