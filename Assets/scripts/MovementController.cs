using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    //Two bools created to check is the player is touching the floor or is in the air
    bool isGrounded = false;
    bool isFloating = false;

    //Two variables to control the speed that the player moves and the height of the jump
    public float speed = 100;
    public float jump = 8.0f;

    //Defines the name of the objects on the Player unity object
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer rot;
    Vector3 moveVector;
    Vector3 jumpForce;

    //Instead of using rigibbody forces creates a constant vector for the gravity
    Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    
    //When the object is started
    void Awake()
    {
        //Puts the components of the objects on the objects defined above
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rot = GetComponent<SpriteRenderer>();

        //Since gravity is now a vector the rigid body gravity is not used, hence, 0 to not affect other functions (if it was bigger than 1 it would always be dragging down and the isGrounded wouldn't work)
        rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        //The speed of the player is the vector defined on the Update function
        rb.velocity = moveVector;

        //If there's an animation
        if (anim)
        {
            //defines hAxis and gives it the value of the input (-1, 0 or 1)
            float hAxis = Input.GetAxis("Horizontal");

            //if the hAxis is not 0 (meaning that is either moving right or left)  
            if (hAxis != 0.0f)
            {
                //Sets the Animator float to 1 to be used on the unity animator to switch between animations and sprites
                anim.SetFloat("Speed", 1);
            }
            //if the calue is 0
            else
            {
                //Sets the Animator float to 0 to be used on the unity animator to switch between animations and sprites
                anim.SetFloat("Speed", 0);
            }

        }
    }
    private void Update()
    {
        //I'm not sure what this does, go ask João David, our Lord and saviour... or a teacher
        moveVector = Vector3.zero;

        //The X value of the movement vector of the player is the input (a,d, right and left) multimplied by the speed of the player 
        moveVector.x = Input.GetAxis("Horizontal") * speed;




        //______________________________________Verification if the player is on the air_______________________________________

        //Checks if the playes is in the air
        if (isFloating)
            //Casts a ray that checks if the top of the charachter hitbox is touching the celling
            isGrounded = Physics2D.Raycast(transform.position + new Vector3(0, GetComponent<CapsuleCollider2D>().bounds.extents.y, 0f), gravity.normalized, 0.5f, ~(1 << LayerMask.NameToLayer("Player")));
        else
            //Casts a ray that checks if the bottom of the character hitbox is touching the floor
            isGrounded = Physics2D.Raycast(transform.position - new Vector3(0, GetComponent<CapsuleCollider2D>().bounds.extents.y , 0f), gravity.normalized, 0.5f, ~(1 << LayerMask.NameToLayer("Player")));




        //_______________________________Verification if the player is on the ground or celling________________________________

        //checks if the player is touching the celling or the floor
        if (isGrounded)
            //Constantly pulls the player down the value of the gravity times the time
            jumpForce.y = gravity.y * Time.deltaTime;
        else
            //if the player is not touching the ground pulls the player 100 times more
            jumpForce.y += 100 * gravity.y * Time.deltaTime;




        //__________________________________________Jump and GravitySwitch inputs________________________________________________


        //Cheks if the SpaceBar is being pressed and the player is touching the floor or celling
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            //depending on the player being upside down or not makes a jump on the negativa or positive Y
            jumpForce.y = isFloating ? -jump : jump;
        }

        //Checks if the W key is down and the player is touching the floor or celling
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            //Inverts the gravity
            gravity *= -1;

            //inverts the isFloating and the rotation on the Y
            isFloating = !isFloating;
            rot.flipY = !rot.flipY;
        }




        //Interpolates the jump force with the vector.zero and the time
        jumpForce = Vector3.Lerp(jumpForce, Vector3.zero, Time.deltaTime);

        //Adds the Jump vector to the player movement
        moveVector += jumpForce;




        //_____________________________________Rotates the player depending on the direction_________________________________________

        //If the X of the player is negative and the rotation of the Y is 0
        if (moveVector.x < 0.0f && transform.rotation.y == 0.0f)
            //Rotates the player 180 degrees on the Y
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
        
        //If the X of the player is positive and the rotation of the Y is negative
        else if (moveVector.x > 0.0f && transform.rotation.y < 0.0f)
            //Rotates the player back to his original rotation
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, -180.0f, 0.0f);
        

    }
}

