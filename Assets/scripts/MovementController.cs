﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    //Two bools created to check is the player is touching the floor or is in the air
    [SerializeField] bool gravityFlip = false;
    [SerializeField] bool isGrounded;
    [SerializeField] bool Jump;
    [SerializeField] float speed = 450;
    [SerializeField] float jump = 730.0f;
    [SerializeField] float MaxJumpTime = 30.0f;
    [SerializeField] float Timer;

    //Defines the name of the objects on the Player unity object
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer rot;
    Vector3 moveVector;
    Vector3 jumpForce;
    CapsuleCollider2D collider;

    //Instead of using rigibbody forces creates a constant vector for the gravity
    Vector3 gravity = new Vector3(0f, -40.0f, 0f);

    bool onGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 10.0f, LayerMask.GetMask("Ground"));
            return (collider != null);
        }
    }

    //When the object is started
    void Awake()
    {
        //Puts the components of the objects on the objects defined above
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();

        //Since gravity is now a vector the rigid body gravity is not used, hence, 0 to not affect other functions (if it was bigger than 1 it would always be dragging down and the isGrounded wouldn't work)
        rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        isGrounded = onGround;

        moveVector = rb.velocity;

        moveVector = new Vector3(Input.GetAxis("Horizontal") * speed, moveVector.y);
        Debug.Log(gravity.y);
        if (Jump)
        {
            if (isGrounded)
            {
                Timer = 0;
                moveVector.y = gravityFlip ? -jump : jump;
            }
            Timer++;

            gravity.y = gravityFlip ? 10.0f +Timer *2: -10.0f - Timer *2;
        }
        else
        {
            gravity.y = gravityFlip ? 40.0f : -40.0f;
        }

        if (isGrounded)
        {
            moveVector.y += gravity.y * Time.deltaTime;
        }
        else
        {
            moveVector.y += 100 * gravity.y * Time.deltaTime;
        }

        moveVector.x = Input.GetAxis("Horizontal") * speed;

        //jumpForce = Vector3.Lerp(jumpForce, Vector3.zero, Time.deltaTime);

        rb.velocity = moveVector;

        bool Fall = (!Jump && !onGround);

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Jump", Jump);
        anim.SetBool("Fall", Fall);
    }

    private void Update()
    {
        
        Jump = (Input.GetButton("Jump"));

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            gravityFlip = !gravityFlip;
            gravity.y *= -1;
            

            if (gravityFlip)
            {
                transform.rotation = new Quaternion(0.0f, 0.0f, 180.0f, 0.0f);
                transform.position = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);
            }
            else
            {
                transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                transform.position = new Vector3(transform.position.x, transform.position.y - 100, transform.position.z);
            }
        }
        

        if (!gravityFlip)
        {
            if (moveVector.x < 0.0f && transform.rotation.y == 0.0f)
                //Rotates the player 180 degrees on the Y
                transform.rotation = transform.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);

            //If the X of the player is positive and the rotation of the Y is negative
            else if (moveVector.x > 0.0f && transform.rotation.y < 0.0f)
                //Rotates the player back to his original rotation
                transform.rotation = transform.rotation * Quaternion.Euler(0.0f, -180.0f, 0.0f);
        }
        else if (gravityFlip)
        {
            if (moveVector.x > 0.0f && transform.rotation.z > 0.0f)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            else if (moveVector.x < 0.0f && transform.rotation.x < 0.0f)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0.0f, -180.0f, 0.0f);
            }
        }
    }
}

