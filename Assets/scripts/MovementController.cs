using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] bool  gravityFlip;
    [SerializeField] bool  isGrounded;
    [SerializeField] bool  Jump;
    [SerializeField] bool  Fall;
    [SerializeField] float speed =        450.0f;
    [SerializeField] float jump =         650.0f;
    [SerializeField] float MaxJumpTime =  20.0f;
    [SerializeField] float jumpCD =       0.0f;
    [SerializeField] float Maxgravity =   25.0f;
    [SerializeField] float Timer;
    [SerializeField] float ToIdleTimer =  0;

    //Defines the name of the objects on the Player unity object
    Rigidbody2D         rb;
    Animator            anim;
    Vector3             moveVector;
    Vector3             jumpForce;
    public Transform    platform;

    //Instead of using rigibbody forces creates a constant vector for the gravity
    Vector3 gravity = new Vector3(0f, -20.0f, 0f);

    bool onGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 10.0f, LayerMask.GetMask("Ground"));
            return (collider != null);
        }
    }
    bool platformHit
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(platform.position, 2.0f, LayerMask.GetMask("Ground"));
            return (collider != null);
        }

    }

    void Awake()
    {
        //Puts the components of the objects on the objects defined above
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Since gravity is now a vector the rigid body gravity is not used, hence, 0 to not affect other functions (if it was bigger than 1 it would always be dragging down and the isGrounded wouldn't work)
        rb.gravityScale = 0;
        gravityFlip = false;
    }

    void FixedUpdate()
    {
        //move inputs and timers to update
        isGrounded = onGround;

        moveVector = rb.velocity;
        moveVector = new Vector3(Input.GetAxis("Horizontal") * speed, moveVector.y);

        if (Jump)
        {
            if (isGrounded)
            {
                Timer = 0;
                moveVector.y = gravityFlip ? -jump : jump;
            }
            Timer ++;

            gravity.y = gravityFlip ? 1.0f +Timer *1.5f: -1.0f - Timer *1.5f;

            if (Timer >= MaxJumpTime) jumpCD = 0;
        }
        else
            gravity.y = gravityFlip ? Maxgravity : -Maxgravity;

        if (platformHit)
        {
            gravity.y = gravityFlip ? Maxgravity : -Maxgravity;
            jumpCD = 0;
        }

        if (isGrounded)
        {
            moveVector.y += gravity.y * Time.deltaTime;
            jumpCD = Mathf.Min(jumpCD + 1, 10);
        }
        else
            moveVector.y += 100 * gravity.y * Time.deltaTime;
        
        Fall = (!Jump && !onGround);

        rb.velocity = moveVector;

        float positiveVelocityX = Mathf.Abs(rb.velocity.x);

        if (positiveVelocityX < 1)
            ToIdleTimer = Mathf.Min(ToIdleTimer + 1, 100);
        else if (positiveVelocityX > 1)
            ToIdleTimer = 0;

        anim.SetFloat("Speed", positiveVelocityX);
        anim.SetFloat("Speedy", Mathf.Abs(rb.velocity.y));
        anim.SetBool("Jump", Jump);
        anim.SetBool("Fall", Fall);
        anim.SetFloat("ToIdleTimer", ToIdleTimer);
        if (ToIdleTimer > 0)
            anim.SetInteger("Random", Random.Range(0,100));
    }

    private void Update()
    {
        Jump = (Input.GetButton("Jump") && jumpCD >= 10);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            gravityFlip = !gravityFlip;
            gravity.y *= -1;

            if (gravityFlip)
            {
                transform.rotation = new Quaternion(0.0f, 0.0f, 180.0f, 0.0f);
                transform.position = new Vector3(transform.position.x, transform.position.y + 150, transform.position.z);
            }
            else
            {
                transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                transform.position = new Vector3(transform.position.x, transform.position.y - 150, transform.position.z);
            }
        }
        

        if (!gravityFlip)
        {
            if (moveVector.x < 0.0f && transform.rotation.y == 0.0f)
                transform.rotation = transform.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);

            else if (moveVector.x > 0.0f && transform.rotation.y < 0.0f)
                transform.rotation = transform.rotation * Quaternion.Euler(0.0f, -180.0f, 0.0f);
        }
        else if (gravityFlip)
        {
            if (moveVector.x > 0.0f && transform.rotation.z > 0.0f)
                transform.rotation = transform.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);

            else if (moveVector.x < 0.0f && transform.rotation.x < 0.0f)
                transform.rotation = transform.rotation * Quaternion.Euler(0.0f, -180.0f, 0.0f);
        }
    }
}

