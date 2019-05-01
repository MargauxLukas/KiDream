using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float explosionTime;
    [SerializeField] float timer;

    bool canHurtBoss = false;

    CircleCollider2D collider;
    GameObject        Boss;

	void Start ()
    {
        Boss = GameObject.Find("Boss");
        collider = gameObject.GetComponent<CircleCollider2D>();
	}
	
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= explosionTime)
        {
            Explode();
        }
	}

    void Explode(Collision2D collision)
    {
        //Debug.Log(collision);
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Boss" && canHurtBoss)
        {
            Destroy(gameObject);
            Boss.GetComponent<Boss>().Damages();
        }
    }

    void Explode()
    {
        if (timer >= explosionTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        canHurtBoss = true;
    }
}
