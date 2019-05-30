using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapeshift : MonoBehaviour
{
    Animator anim;
    public RuntimeAnimatorController normalAnim;
    public RuntimeAnimatorController enemyAnim;
    [SerializeField] SpriteRenderer shapeshiftSprite;
    [SerializeField] float shapeshiftDuration = 5.0f;

    public sightController sight;

    public float shapeshiftTimer;

    public bool isShapeshifted
    {
        get
        {
            return shapeshiftTimer > 0.0f;
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
        if (Input.GetKeyDown(KeyCode.Q) && sight.hasEnemiesWithin)
        {
            shapeshiftTimer = shapeshiftDuration;
        }
        
        if (shapeshiftTimer > 0.0f)
        {
            shapeshiftTimer -= Time.deltaTime;
        }

        if (isShapeshifted)
            anim.runtimeAnimatorController = enemyAnim;

        if (shapeshiftTimer <= 0.0f)
            anim.runtimeAnimatorController = normalAnim;
    }
}
