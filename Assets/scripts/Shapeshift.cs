using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapeshift : MonoBehaviour
{
    [SerializeField] SpriteRenderer shapeshiftSprite;
    [SerializeField] float shapeshiftDuration = 5.0f;

    public float shapeshiftTimer;

    public bool isShapeshifted
    {
        get
        {
            if (shapeshiftTimer > 0.0f)
            {
                return true;
            }

            return false;
        }
    }


    void Start()
    {
        shapeshiftSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isShapeshifted);

        if (Input.GetKeyDown(KeyCode.Q) )
        {
            shapeshiftTimer = shapeshiftDuration;
        }
        
        if (shapeshiftTimer > 0.0f)
        {
            shapeshiftTimer -= Time.deltaTime;
        }

        if (isShapeshifted) shapeshiftSprite.color = Color.red;

        if (shapeshiftTimer <= 0.0f) shapeshiftSprite.color = Color.white;
    }
}
