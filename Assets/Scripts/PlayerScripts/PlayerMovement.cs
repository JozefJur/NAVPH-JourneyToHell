using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 15f;
    public float baseMovementSpeed = 15f;


    private float orientation = 1;
    private CharacterMovementController Player;
    private Rigidbody2D rigidBody;
    private float movementAxis;

    void FixedUpdate()
    {
        transform.position += new Vector3(movementAxis, 0, 0) * Time.deltaTime * movementSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.GetComponent<CharacterMovementController>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementAxis = Input.GetAxis("Horizontal");
        //transform.position += new Vector3(movementAxis, 0, 0) * Time.deltaTime * movementSpeed;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            orientation = Input.GetKeyDown(KeyCode.A) ? -1 : 1;
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
