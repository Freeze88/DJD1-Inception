using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTo;
    public Transform Player;
    [SerializeField] float Timer;
    bool InsidePortal;
    // Tentativa de fazer timer para os portais
    // float PortalTimerCurrent = 0.0f;
    // float PortalTimerStart = 3.0f;

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
        // Tentativa de fazer timer para os portais
        // PortalTimerCurrent = PortalTimerStart;
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

        // Tentativa da contagem dos segundos do timer
        // PortalTimerCurrent -= 1 * Time.deltaTime;

        // Checks if the player is inside the portal
        // and if the E key is pressed 
        // teleports him afterwards
        if (InsidePortal && Input.GetKeyDown(KeyCode.E)) //&& PortalTimerCurrent == 0.0f) - condiçao do timer
        {
            Player.position = teleportTo.position;
            // Tentativa de dar reset ao timer
            // PortalTimerCurrent = PortalTimerStart;
        }
    }
}
