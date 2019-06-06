using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapeshift : MonoBehaviour
{
    Animator anim;
    public RuntimeAnimatorController normalAnim;
    public RuntimeAnimatorController enemyAnim;
    [SerializeField] SpriteRenderer shapeshiftSprite;
    [SerializeField] float shapeshiftDuration = 10.0f;

    public sightController sight;

    float shapeshiftTimer;

    public bool isShapeshifted
    {
        get
        {
            return shapeshiftTimer > 5.0f;
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        shapeshiftSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Debug.Log("Has enemies:" + sight.hasEnemiesWithin);
        if (Input.GetButtonDown("Fire2") && sight.hasEnemiesWithin && shapeshiftTimer <= 0)
        {
            shapeshiftTimer = shapeshiftDuration;
        }

        if (shapeshiftTimer > 0)
            shapeshiftTimer = Mathf.Min(shapeshiftTimer - 0.02f, shapeshiftDuration);


        if (isShapeshifted)
            anim.runtimeAnimatorController = enemyAnim;

        if (shapeshiftTimer <= 5.0f)
            anim.runtimeAnimatorController = normalAnim;
    }

    public float GetTimer()
    {
        return shapeshiftTimer / shapeshiftDuration;
    }
}
