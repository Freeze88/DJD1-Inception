using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    //Creates a variable for the Input
    float Yaxis;

    //Looks for an Object called Player
    public GameObject Player;
    
    //Creates two constants for the X and Y limits of the camera movement
    public float Xlimit = 150.0f;
    public float Ylimit = 100.0f;

    //*** NOTE: The camera movement is better to be made on FixedUpdate since it's called every cycle instead of every frame, a change is bound to be made***//

    // Update is called once per frame
    void Update()
    {
        //Defines Yaxis as the Input (w,s, up and down)
        Yaxis = Input.GetAxis("Vertical");


        //____________________________________________________Camera Y movement______________________________________________________________________

        //checks if the vertical position of the player is bigger than the vertical position of the camera plus the limit of distance between the two.
        if (Player.transform.position.y >= transform.position.y + Ylimit)
        {
            //Makes the camera position the same as the player with the offset defined by the limit
            transform.position = new Vector3(transform.position.x, Player.transform.position.y - Ylimit, transform.position.z);
        }

        //checks if the vertical position of the player is smaller than the vertical position of the camera minus the limit of distance between the two.
        else if (Player.transform.position.y <= transform.position.y - Ylimit)
        {
            //Makes the camera position the same as the player with the offset defined by the limit
            transform.position = new Vector3(transform.position.x, Player.transform.position.y + Ylimit, transform.position.z);
        }



        //____________________________________________________Camera X movement______________________________________________________________________

        //checks if the horizontal position of the player is bigger than the horizontal position of the camera plus the limit of distance between the two.
        if (Player.transform.position.x >= transform.position.x + Xlimit)
        {
            //Makes the camera position the same as the player with the offset defined by the limit
            transform.position = new Vector3(Player.transform.position.x - Xlimit, transform.position.y, transform.position.z);
        }

        //checks if the horizontal position of the player is smaller than the horizontal position of the camera minus the limit of distance between the two.
        else if (Player.transform.position.x <= transform.position.x - Xlimit)
        {
            //Makes the camera position the same as the player with the offset defined by the limit
            transform.position = new Vector3(Player.transform.position.x + Xlimit, transform.position.y, transform.position.z);
        }
        

    }
}
