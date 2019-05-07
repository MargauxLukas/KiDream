using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private int damage = 0;

    private Transform target;

    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	void Update ()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        /*if(transform.position.x == target.position.x && transform.position.y == target.position.y)
        {
            DestroyProjectile();
        }*/
	}

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<CharacterController>().hp = collision.GetComponent<CharacterController>().hp - damage;
            Debug.Log(collision.GetComponent<CharacterController>().hp);
            DestroyProjectile();
        }
    }
}

