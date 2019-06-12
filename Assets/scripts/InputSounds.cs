using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSounds : MonoBehaviour
{
    public AudioSource morphSound;
    public AudioSource gravSound; 
    public AudioSource jumpSound;
    public AudioSource walkSound;
    public AudioSource doorSound;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            morphSound.Play();
        if (Input.GetKeyDown(KeyCode.W))
            gravSound.Play();
        if (Input.GetKeyDown(KeyCode.Space))
            jumpSound.Play();
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            walkSound.Play();
        if (Input.GetKeyDown(KeyCode.E))
            doorSound.Play();
    }
}