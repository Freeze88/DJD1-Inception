﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    Animator anim;
    GameObject Player;
    Collider2D doorCollider;
    static ButtonAction Button;

    [SerializeField] float Timer;
    bool InsidePortal;
    public AudioSource doorSound;

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
        anim = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        if (Button == null)
            Button = GameObject.Find("Button").GetComponent<ButtonAction>();
    }

    void FixedUpdate()
    {
        Timer = Mathf.Max(Timer - 1, 0);
    }

    void Update()
    {
        InsidePortal = isIn;

        if (InsidePortal && Input.GetButtonDown("Fire1"))
        {
            Application.Quit();
            doorSound.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Button.anim.SetInteger("Transition", 1);

            Button.transform.position = doorCollider.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Button.anim.SetInteger("Transition", 2);
        }
    }
}