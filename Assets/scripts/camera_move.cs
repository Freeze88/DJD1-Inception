using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    float Yaxis;
    public GameObject Player;

    private Vector3 offset;

    public float limit = 150.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Yaxis = Input.GetAxis("Vertical");

        /*if (Yaxis > 0.0f && transform.rotation.z == 0.0f)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }
        else if (Yaxis < 0.0f && transform.rotation.z > 0.0f)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0.0f, 0.0f, -180.0f);
        }*/

        if (Player.transform.position.x >= transform.position.x + limit)
            transform.position = new Vector3 (Player.transform.position.x - limit, transform.position.y, transform.position.z);

        else if (Player.transform.position.x <= transform.position.x - limit)
            transform.position = new Vector3(Player.transform.position.x + limit, transform.position.y, transform.position.z);
        

    }
}
