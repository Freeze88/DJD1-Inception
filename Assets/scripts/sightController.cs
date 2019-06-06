using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sightController : MonoBehaviour
{
    List<GameObject> EnemiesWithin = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemiesWithin.Add(collision.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemiesWithin.Remove(collision.gameObject);
        }

    }
    public bool hasEnemiesWithin
    {
        get
        {
            Vector3 vector = GetComponent<CircleCollider2D>().transform.position;
            Debug.Log(vector);

            foreach (GameObject go in EnemiesWithin)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(vector.x, vector.y) , go.transform.position - transform.position, Vector2.Distance(go.transform.position, transform.position), ~((1<< LayerMask.NameToLayer("sight")) | (1<< LayerMask.NameToLayer("Player"))));

                if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log(hit.collider.gameObject.name);
                    return true;
                }
            }
            return false;
        }
    }
}
