using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterMovementController : MonoBehaviour
{

    public float movementSpeed = 15f; // player movement speed
    public float jumpForce = 10f;  // player jump force
    public float dodgeForce = 20f; // player dodge force
    public float dashDuration = 0.3f; // player dash duration
    public int jumpNumber = 1;
    public List<ItemTemplate> allItems = new List<ItemTemplate>();
    public int maxHealth = 100;
    public int currHealth;


    private float dashCooldown = 0;

    private Rigidbody2D rigidBody;  // rigid body instance

    private int currJumpLeft = 1; // check if player jumped

    private float currentDashTimer = 0;

    private DashState dash = DashState.READY;

    private float orientation = 1;

    private Vector2 accVelocity;

    private bool inJump = false;

    private float currSpeed;
   
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
        currSpeed = movementSpeed;
       /* var items = FindObjectsOfType<Object>().OfType<ItemTemplate>();
        foreach (ItemTemplate item in items)
        {
            allItems.Add(item);
        }
        //Debug.Log(allItems[0].getStacks());
        allItems[0].addStack();*/
        //Debug.Log(allItems[0].getStacks());
    }

    void Update()
    {
        //Debug.Log(jumpNumber);
        //Debug.Log(movementSpeed + " " + jumpNumber);
        foreach (ItemTemplate item in allItems)
        {
            if (item.isReady())
            {
                var func = item.getEffectFunction();
                func(this);
            }
            else
            {
                item.coolDown();
            }
        }
        
        preformMovement();

        preformJump();

        preformDash();
        
        performSprint();
    }

    private void preformMovement()
    {
        var movementAxis = Input.GetAxis("Horizontal");
        
        transform.position += new Vector3(movementAxis, 0, 0) * Time.deltaTime * movementSpeed;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            orientation = Input.GetKeyDown(KeyCode.A) ? -1 : 1;
        }
    }

    private void preformJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currJumpLeft > 0)
        {
            inJump = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            currJumpLeft--;
        }
    }

    private void preformDash()
    {
        switch (dash)
        {
            case DashState.READY:
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    dash = DashState.IN_PROGRESS;
                    accVelocity = rigidBody.velocity;
                    rigidBody.velocity = new Vector2(orientation * dodgeForce, rigidBody.velocity.y);
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

    private void performSprint(){
        if(Input.GetKey(KeyCode.LeftShift) && (currSpeed == movementSpeed) && (!inJump)){
            currSpeed = movementSpeed;
            movementSpeed += 25f;
        }
        else{
            movementSpeed = currSpeed;
        }
    }

    public void addItem(ItemTemplate item)
    {
        //Debug.Log(item is Feather);

        bool containsItem = false;

        foreach(ItemTemplate itemC in allItems){
            if(itemC.GetType().Equals(item.GetType()))
            {
                itemC.addStack();
                containsItem = true;
            }
        }

        if (!containsItem)
        {
            item.addStack();
            allItems.Add(item);
        }
    }

    public void getDamage(int dmg){
        currHealth -= dmg;
        if (currHealth <= 0){
            currHealth = 0;
        }
    }

    public void heal(int hp){
        currHealth += hp;
        if (currHealth > maxHealth){
            currHealth = maxHealth;
        }
        Debug.Log($"Curr health - {currHealth}, max health - {maxHealth}");
    }

    public void maxHealthUp(int hp){
        maxHealth += hp;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Vector3 direction = transform.position - col.gameObject.transform.position;
            if(direction.y > 0)
            {
                inJump = false;
                currJumpLeft = jumpNumber;
                Debug.Log($"Collision with {col.gameObject.tag}");}
            }
    }

    public enum DashState
    {
        READY,
        IN_PROGRESS,
        ON_COOLDOWN
    }

}
