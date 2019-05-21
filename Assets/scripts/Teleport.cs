using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTo;
    public Transform Player;
    [SerializeField]float Timer;
    bool InsidePortal;

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
        
        InsidePortal = isIn;

        // Checks if the player is inside the portal
        // and if the E key is pressed 
        // teleports him afterwards
        if (InsidePortal && Input.GetKey(KeyCode.E))
        {
            Player.position = teleportTo.position;
        }
    }
}
