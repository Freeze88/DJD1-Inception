﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    [HideInInspector] public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
