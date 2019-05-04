using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator animator;
    [SerializeField] float explosionTime;
    [SerializeField] float timer;

    bool canHurtBoss = false;

    private bool isDream = true;

    CircleCollider2D collider;

    GameObject boss;
    GameObject player;

	void Start ()
    {
        animator = GetComponent<Animator>();
        boss     = GameObject.Find("Boss");
        player   = GameObject.Find("Player");
        collider = gameObject.GetComponent<CircleCollider2D>();
	}
	
	void Update ()
    {
        isDream = player.GetComponent<CharacterController>().isDream;
        if (isDream) { animator.SetBool("isDream",  true);}
        else         { animator.SetBool("isDream", false);}
        timer += Time.deltaTime;

        if (timer >= explosionTime - 4.380f)
        {
            animator.SetBool("isTimer",true);
        }
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
            animator.SetBool("isExplode", true);
            Destroy(collision.gameObject);
            Destroy(gameObject,0.6f);
        }
        if (collision.gameObject.tag == "Boss" && canHurtBoss)
        {
            animator.SetBool("isExplode", true);
            boss.GetComponent<Boss>().Damages();
            Destroy(gameObject,0.55f);
        }
        if(collision.gameObject.name == "WallCollider")
        {
            animator.SetBool("isExplode", true);
            Destroy(gameObject, 0.55f);
        }
    }

    void Explode()
    {
        Destroy(gameObject);
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
