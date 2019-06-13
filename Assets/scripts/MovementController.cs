using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] bool gravityFlip = false;
    [SerializeField] bool isGrounded;
    [SerializeField] bool JumpIsPressed;
    [SerializeField] float speed = 450.0f;
    [SerializeField] float jump = 650.0f;
    [SerializeField] float MaxJumpTime = 20.0f;
    [SerializeField] float jumpCD = 0.0f;
    [SerializeField] float Maxgravity = 2250.0f;
    [SerializeField] float Timer;
    [SerializeField] float ToIdleTimer = 0;
    [SerializeField] float V_input;
    [SerializeField] float H_input;
    [SerializeField] bool GravityIsPressed;
    [SerializeField] bool flipped = false;
    
    //Defines the name of the objects on the Player unity object
    Rigidbody2D rb;
    Animator anim;
    Vector3 moveVector;
    Vector3 jumpForce;
    BoxCollider2D BCollider;
    CapsuleCollider2D CCollider;
    public Transform platform;
    Coroutine flipCoroutine;
    public AudioSource gravSound;
    public AudioSource jumpSound;
    public AudioSource walkSound;

    //walkSound.volume = Random.Range(0.6f, 1);
    //walkSound.pitch = Random.Range(0.6f, 1.2f);
    //walkSound.Play();

    //Instead of using rigibbody forces creates a constant vector for the gravity
    Vector3 gravity = new Vector3(0f, -2250.0f, 0f);

    bool onGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 15.0f, LayerMask.GetMask("Ground"));
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
        BCollider = GetComponent<BoxCollider2D>();
        CCollider = GetComponent<CapsuleCollider2D>();
        //Since gravity is now a vector the rigid body gravity is not used, hence, 0 to not affect other functions (if it was bigger than 1 it would always be dragging down and the isGrounded wouldn't work)
        rb.gravityScale = 0;
        gravityFlip = false;
    }

    void FixedUpdate()
    {
        //move inputs and timers to update
        isGrounded = onGround;

        moveVector = rb.velocity;
        moveVector = new Vector3(H_input * speed, moveVector.y);

        if (JumpIsPressed)
        {
            if (isGrounded)
            {
                Timer = 0;
                moveVector.y = gravityFlip ? -jump : jump;
                jumpSound.volume = Random.Range(0.6f, 1);
                jumpSound.pitch = Random.Range(0.8f, 1);
                jumpSound.Play();
            }
            Timer++;

            gravity.y = gravityFlip ? 1.0f + Timer * 130f : -1.0f - Timer * 130f;

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
            if (BCollider.enabled)
            {
                BCollider.enabled = false;
                CCollider.enabled = true;
            }
            jumpCD = Mathf.Min(jumpCD + 1, 10);
        }
        else
        {
            if (!BCollider.enabled)
            {
                CCollider.enabled = false;
                BCollider.enabled = true;
            }
        }

        moveVector.y += gravity.y * Time.fixedDeltaTime;

        rb.velocity = moveVector;

        float positiveVelocityX = Mathf.Abs(rb.velocity.x);

        if (positiveVelocityX < 1)
            ToIdleTimer = Mathf.Min(ToIdleTimer + 1, 100);
        else if (positiveVelocityX > 1)
            ToIdleTimer = 0;

        anim.SetFloat("Speed", positiveVelocityX);
        anim.SetFloat("Speedy", Mathf.Abs(rb.velocity.y));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("Jump", JumpIsPressed);
        anim.SetFloat("ToIdleTimer", ToIdleTimer);
        if (ToIdleTimer > 0)
            anim.SetInteger("Random", Random.Range(0, 100));
    }

    private void Update()
    {
        JumpIsPressed = (Input.GetButton("Jump") && jumpCD >= 10);
        V_input = Input.GetAxis("Vertical");
        H_input = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !GravityIsPressed)
        {
            gravityFlip = !gravityFlip;
            gravity.y = gravityFlip ? -Maxgravity : Maxgravity;
            GravityIsPressed = true;
            gravSound.Play();
            if (flipCoroutine != null)
                StopCoroutine(flipCoroutine);
            flipCoroutine = null;
        }

        if (GravityIsPressed && flipCoroutine == null)
        {
            flipCoroutine = StartCoroutine(flip());
        }


        Vector3 euler = transform.rotation.eulerAngles;
        if (!flipped)
        {
            if (moveVector.x < 0)
            {
                transform.rotation = Quaternion.Euler(euler.x, 180, euler.z);
            }
            else if (moveVector.x > 0)
            {
                transform.rotation = Quaternion.Euler(euler.x, 0, euler.z);
            }
        }
        else
        {
            if (moveVector.x < 0)
            {
                transform.rotation = Quaternion.Euler(euler.x, 0, euler.z);
            }
            else if (moveVector.x > 0)
            {
                transform.rotation = Quaternion.Euler(euler.x, 180, euler.z);
            }
        }
    }

    private IEnumerator flip()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + (gravityFlip ? 10 : -10));
        RaycastHit2D hit2 = Physics2D.Raycast(pos, transform.up, 5000f, LayerMask.GetMask("Ground"));

        float dis = Mathf.Abs(pos.y - hit2.point.y);

        while (Mathf.Abs(transform.position.y - hit2.point.y) > (dis  / 1.5f) +20)
        {
            pos.x = transform.position.x;
            hit2 = Physics2D.Raycast(pos, transform.up, 5000f, LayerMask.GetMask("Ground"));

            yield return null;
        }

        transform.rotation = new Quaternion(0.0f, 0.0f, (gravityFlip ? 180 : 0), 0.0f);
        Vector3 vec = transform.position;
        vec.y = vec.y + (gravityFlip ? 90 : -90);
        transform.position = vec;
        flipped = !flipped;
        flipCoroutine = null;
        GravityIsPressed = false;
    }
    public float GetVelocityY()
    {
        return Mathf.Abs(rb.velocity.y);
    }
    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}

