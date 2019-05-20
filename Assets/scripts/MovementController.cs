﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    //Two bools created to check is the player is touching the floor or is in the air
    [SerializeField] bool gravityFlip = false;
    [SerializeField] bool isGrounded;
    [SerializeField] bool Jump;
    [SerializeField] float speed = 100;
    [SerializeField] float jump = 8.0f;

    //Defines the name of the objects on the Player unity object
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer rot;
    Vector3 moveVector;
    Vector3 jumpForce;

    //Instead of using rigibbody forces creates a constant vector for the gravity
    Vector3 gravity = new Vector3(0f, -15.0f, 0f);

    bool onGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.0f, LayerMask.GetMask("Ground"));
            return (collider != null);
        }
    }

    //When the object is started
    void Awake()
    {
        //Puts the components of the objects on the objects defined above
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Since gravity is now a vector the rigid body gravity is not used, hence, 0 to not affect other functions (if it was bigger than 1 it would always be dragging down and the isGrounded wouldn't work)
        rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        isGrounded = onGround;

        if (Jump)
        {
            jumpForce.y = gravityFlip ? -jump : jump;
        }
        if (!isGrounded)
        {
            jumpForce.y += 100 * gravity.y * Time.deltaTime;
        }
        else
        {
            jumpForce.y += gravity.y * Time.deltaTime;
        }

        moveVector = Vector3.zero;

        moveVector.x = Input.GetAxis("Horizontal") * speed;

        jumpForce = Vector3.Lerp(jumpForce, Vector3.zero, Time.deltaTime);

        moveVector += jumpForce;

        rb.velocity = moveVector;

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void Update()
    {

        Jump = (Input.GetKey(KeyCode.Space) && isGrounded);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            gravityFlip = !gravityFlip;
            gravity.y *= -1;
            transform.rotation.x 
        }

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

