using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    bool isGrounded = false;
    public float speed = 100;
    public float jump = 8.0f;
    bool isFloating = false;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer rot;
    Vector3 moveVector;
    Vector3 jumpForce;
    Vector3 currentVelocity;

    Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rot = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0;
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        currentVelocity = moveVector;

        rb.velocity = currentVelocity;

        if (anim)
        {
            float hAxis = Input.GetAxis("Horizontal");

            if (hAxis > 0.0f || hAxis < 0.0f)
            {
                anim.SetFloat("Speed", 1);
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }

        }
    }
    private void Update()
    {
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * speed;

        if (isFloating)
            isGrounded = Physics2D.Raycast(transform.position + new Vector3(0, GetComponent<CapsuleCollider2D>().bounds.extents.y, 0f), gravity.normalized, 0.5f, ~(1 << LayerMask.NameToLayer("Player")));
        else
            isGrounded = Physics2D.Raycast(transform.position - new Vector3(0, GetComponent<CapsuleCollider2D>().bounds.extents.y , 0f), gravity.normalized, 0.5f, ~(1 << LayerMask.NameToLayer("Player")));

        if (!isGrounded)
            jumpForce.y += 100 * gravity.y * Time.deltaTime;
        else
            jumpForce.y = gravity.y * Time.deltaTime;


        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jumpForce.y = isFloating ? -jump : jump;
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            //rb.gravityScale = -rb.gravityScale;
            gravity *= -1;
            isFloating = !isFloating;
            rot.flipY = !rot.flipY;
        }

        jumpForce = Vector3.Lerp(jumpForce, Vector3.zero, Time.deltaTime);
        moveVector += jumpForce;

        if (moveVector.x < 0.0f && transform.rotation.y == 0.0f)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if (moveVector.x > 0.0f && transform.rotation.y < 0.0f)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, -180.0f, 0.0f);
        }

    }
}

