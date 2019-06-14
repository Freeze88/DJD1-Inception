using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReset : MonoBehaviour
{
    public Transform teleportTo;
    GameObject Player;
    Collider2D doorCollider;

    [SerializeField] float Timer;
    bool InsidePortal;
    public AudioSource resetSound;

    bool isIn
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 20.0f, LayerMask.GetMask("Player"));
            return (collider != null);
        }
    }
    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        Player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Timer = Mathf.Max(Timer - 1, 0);
    }

    void Update()
    {
        InsidePortal = isIn;

        if (InsidePortal)
        {
            Player.transform.position = teleportTo.position;
            resetSound.Play();
        }
    }
}