using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 75.0f;
    [SerializeField] float moveDir = 1.0f;
    [SerializeField] Transform groundSensor;
    [SerializeField] Transform wallSensor;
    [SerializeField] Collider2D grabCollider;
    Rigidbody2D rigidBody;
    SpriteRenderer sprite;
    GameObject teleportTo;
    static GameObject Player;
    static Shapeshift Shapeshift;
    public AudioSource hitSound;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        teleportTo = GameObject.Find("LimboPosition");
        Player = GameObject.Find("Player");
        if (Shapeshift == null)
            Shapeshift = Player.GetComponent<Shapeshift>();
    }

    void Update()
    {
        Vector2 currentVelocity = rigidBody.velocity;
        currentVelocity.x = moveDir * moveSpeed;
        
        rigidBody.velocity = currentVelocity;

        if (moveDir > 0.0f) transform.rotation = Quaternion.identity;
        else transform.rotation = Quaternion.Euler(0, 180, 0);

        Collider2D collider = Physics2D.OverlapCircle(groundSensor.position, 2.0f, LayerMask.GetMask("Ground"));
        
        if (collider == null)
        {
            moveDir = -moveDir;
        }
        else
        {
            collider = Physics2D.OverlapCircle(wallSensor.position, 2.0f, LayerMask.GetMask("Ground"));
            if (collider != null)
            {
                moveDir = -moveDir;
            }
        }
        if (grabCollider)
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.ClearLayerMask();
            filter.SetLayerMask(LayerMask.GetMask("Player"));

            Collider2D[] results = new Collider2D[8];

            int grabCollision = Physics2D.OverlapCollider(grabCollider, filter, results);
           
            if (grabCollision > 0 && !Shapeshift.isShapeshifted)
            {
                {
                    Player.transform.position = new Vector2(teleportTo.transform.position.x,teleportTo.transform.position.y);
                    hitSound.Play();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundSensor)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundSensor.position, 10.0f);
        }

        if (wallSensor)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(wallSensor.position, 10.0f);
        }
    }
}
