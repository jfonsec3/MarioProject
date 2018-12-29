using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteBehavior : MonoBehaviour {

    //Private fields which will not be visible in the inspector
    private Rigidbody2D rb;
    private const float groundRadius = .2f;
    private bool facingRight = true;
    private int currentJumps = 0;
    private bool grounded = true;
    public int PowerUpMode = 0;
    public int coins = 0;
    public int multiplier = 1;
    public int numberOfJumps = 2;

    //Serialized fields (appear in the Unity inspector)
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float movementSpeed = 1.5f;
    [SerializeField]
    private float jumpForce = 10f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print(numberOfJumps);
        grounded = IsGrounded();

        //Checking for jumps
        if (grounded)
        {
            //We are crounded so reset the number of multiple jumps that can occurr
            currentJumps = 0;
            //If we are grounded, then we are always facing right, so ensure this is true
            facingRight = true;
            if (transform.localScale.x < 0)
            {

                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }

            //Normal on ground jump check
            if (Input.GetButtonDown("Jump") && currentJumps < numberOfJumps)
            {
                rb.velocity = Vector2.up * jumpForce;
                currentJumps++;
            }
        }
        else
        {
            //Check for wall jumping and multiple jumps
            CheckJump();
        }
    }

    //Better for handling physics based changes
    void FixedUpdate()
    {
        //Ensure we are always moving in horizontal direction
        rb.velocity = new Vector2((facingRight ? movementSpeed : -movementSpeed) * 10f, rb.velocity.y);
    }

    //Checks for both wall jumping and multiple jumps (within the air already)
    private void CheckJump()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1f);
        if (Input.GetButtonDown("Jump"))
        {
            //Checking for wall jumping
            if (hit.collider != null)
            {
                rb.velocity = new Vector2(hit.normal.x * movementSpeed * 10f, 7f);
                //Switch the direction we are facing.
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            //Handling multiple jumps (if we are not grounded and we are not going to hit a wall)
            else if (currentJumps < numberOfJumps)
            {

                rb.velocity = Vector2.up * jumpForce * 1.5f;
                currentJumps++;
            }


        }
    }

    //Checks to see if the Player is currently on the ground
    private bool IsGrounded()
    {
        //Will check all possible colliders that are overlapping with the given ground check position and what ground has been given to be.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            //If the object is not the player object
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public void size(float mult)
    {
        Vector3 temp = transform.localScale;
        while (transform.localScale.x < temp.x * mult)
        {
           transform.localScale = new Vector3(transform.localScale.x + 0.01f * Time.deltaTime, transform.localScale.y + 0.01f * Time.deltaTime, transform.localScale.z + 0.01f * Time.deltaTime);
        }
        
    }

    public int getMode()
    {
        return PowerUpMode;
    }
    public void setMode(int type)
    {
        PowerUpMode = type;
    }
    public int getCoins()
    {
        return coins;
    }

    public void IncrementCoins()
    {
        coins += multiplier;
    }
}
