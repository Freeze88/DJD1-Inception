using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    //Creates a variable for the Input
    float Yaxis;

    //Looks for an Object called Player
    public MovementController Player;

    public float Yoffset = 100.0f;
    public float cameraSpeed = 5.0f;
    bool MoveCamera = true;
    bool PlayerFall;
    float shakeTimer = 300;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Player.GetVelocityY() > 1000)
            PlayerFall = true;

        if (Player.GetIsGrounded() && PlayerFall)
        {
            StartCoroutine(Shake());
        }
        float offsetX = Input.GetAxis("Horizontal");
        Vector3 offset = new Vector2((offsetX * 200), (Player.transform.rotation.z != 0 ? -Yoffset : Yoffset));
        Vector2 newPos = Player.transform.position + offset;

        float distance = Vector2.Distance(newPos, transform.position);

        if (MoveCamera)
        {
            newPos = Vector2.MoveTowards(transform.position, newPos, (cameraSpeed * distance) * Time.fixedDeltaTime);

            transform.position = newPos;
        }
    }

    private IEnumerator Shake()
    {
        Vector3 shake = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
        transform.position += shake;
        yield return new WaitForSeconds(0.25f);

        PlayerFall = false;

        Debug.Log("im here");
    }
}
