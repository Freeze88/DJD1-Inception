using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    bool isGrounded;
    public Transform teleportTo;
    public Transform Player;
    [SerializeField] float Timer;
    bool InsidePortal;

    bool onGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 10.0f, LayerMask.GetMask("Ground"));
            return (collider != null);
        }
    }
    bool isIn
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 20.0f, LayerMask.GetMask("Player"));
            return (collider != null);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (Timer <= 0)
        {
            Timer = 0;
        }
        else
        {
            Timer--;
        }
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = onGround;
        InsidePortal = isIn;

        if (InsidePortal && Input.GetKeyDown(KeyCode.E) && isGrounded)
        {
            Player.position = teleportTo.position;
        }
    }
}
