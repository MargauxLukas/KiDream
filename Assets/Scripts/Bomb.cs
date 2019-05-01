using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float explosionTime;
    [SerializeField] float timer;

    bool canHurtBoss = false;

    BoxCollider2D collider;
    GameObject        Boss;

	void Start ()
    {
        Boss = GameObject.Find("Boss");
        collider = gameObject.GetComponent<BoxCollider2D>();
	}
	
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= explosionTime)
        {
            Explode();
        }
	}

    void Explode(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Boss" && canHurtBoss)
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

    void OnTriggerEnter2D(Collider2D collision)
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
