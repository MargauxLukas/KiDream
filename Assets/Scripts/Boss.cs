using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    Animator animator;

    public GameObject   bomb;
    private Transform target;
    private Rigidbody2D   rb;

    public  float speed = 0.2f;

    public int       hp =    3;

	void Start ()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        Move();
	}

    void LookAt()
    {
        float xDifference = Mathf.Abs(transform.position.x - target.position.x);
        float yDifference = Mathf.Abs(transform.position.y - target.position.y);

        if (xDifference > yDifference)
        {
            animator.SetFloat("moveY", 0f);
            if (target.position.x > transform.position.x) { animator.SetFloat("moveX",  1f);}
            if (target.position.x < transform.position.x) { animator.SetFloat("moveX", -1f);}

        }
        else if (xDifference < yDifference)
        {
            animator.SetFloat("moveX", 0f);
            if (target.position.y > transform.position.y) { animator.SetFloat("moveY",  1f);}
            if (target.position.y < transform.position.y) { animator.SetFloat("moveY", -1f);}
        }
    }

    void Sleep()
    {
        //Pas sur encore si c'est fait en cinematique
    }

    void Move()
    {
        LookAt();
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void Jump()
    {
        //Le boss saute et atteri détruisant les murs 
    }

    void LauchBomb()
    {
        //Le boss lance ses clochettes, elles explosent au contact du joueur.
    }

    void MobInvoking()
    {
        //Invoque les petits mobs clochettes "Grelottins".
    }

    void PushWave()
    {
        //Push Player/IA/Bombe
    }

    void isDead()
    {

    }
}
