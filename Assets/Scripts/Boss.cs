using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    Animator animator;

    public Rigidbody2D    rb;
    public GameObject   Bomb;
    private Transform target;

    public float speed = 0.5f;
    public int      hp =    3;

	void Start ()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
		
	}
    
    void Sleep()
    {
        //Pas sur encore si c'est fait en cinematique
    }

    void Move()
    {
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
