using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickBehaviour : MonoBehaviour
{
    private bool isDream = true;

    private GameObject player;
    private Animator animator;
    private Transform  target;

    public GameObject pushCorrupted1;
    public GameObject pushCorrupted2;
    public GameObject pushCorrupted3;
    public GameObject pushCorrupted4;

    [Header("A cocher pour l'empêcher de regarder dans une direction")]
    public bool neverUp    = false ;
    public bool neverDown  = false ;
    public bool neverLeft  = false ;
    public bool neverRight = false ;

    [Header("A cocher si on veut qu'il se déplace")]
    public bool isMoving   = false ;
    public float speedMoving = 0.5f;

    [Header("A remplir si on veut qu'il se déplace toutes les [timeBeforeMoving] seconds pendant [timeMoving] seconds")]
    public int timeBeforeMoving = 0; 
    public int timeMoving       = 0;

    private int lookingAt = 0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up
    private int seconds   = 0;
    private int timeAddition ;  //timeBeforeMoving + timeMoving
    private float timer;

    void Start ()
    {
        player       = GameObject.Find("Player");
        animator     = GetComponent<Animator>();
        target       = player.transform;
        timeAddition = timeBeforeMoving + timeMoving;
    }
	
	void Update ()
    {
        isDream = player.GetComponent<CharacterController>().isDream;

        if (isDream) { animator.SetBool("isDream",  true);}
        else         { animator.SetBool("isDream", false);}

        LookAt();
        Attack(lookingAt);

        if(isMoving && timeBeforeMoving == 0)
        {
            Move();
        }
        else if(isMoving && timeBeforeMoving != 0)
        {
            if (Time.time > timer + 1) //Timer
            {
                timer = Time.time;
                seconds++;
            }
            else if(seconds >= timeBeforeMoving && seconds < timeAddition)
            {
                Move();
            }
            else if(seconds == timeAddition)
            {
                seconds = 0;
                animator.SetBool("isMoving", false);
            }
        }
    }

    void Move()
    {
        animator.SetBool("isMoving", true);
        LookAt();
        transform.position = Vector2.MoveTowards(transform.position, target.position, speedMoving * Time.deltaTime);
    }


    void LookAt()
    {
        float xDifference = Mathf.Abs(transform.position.x - target.position.x);
        float yDifference = Mathf.Abs(transform.position.y - target.position.y);

        if (xDifference > yDifference)
        {
            animator.SetFloat("moveY", 0f);

            if (target.position.x > transform.position.x && !neverRight)
            {
                animator.SetFloat("moveX",  1f);
                lookingAt = 1;                 //Droite
            }
            if (target.position.x < transform.position.x && !neverLeft)
            {
                animator.SetFloat("moveX", -1f);
                lookingAt = 3;                 //Gauche
            }
        }
        else if (xDifference < yDifference)
        {
            animator.SetFloat("moveX", 0f);

            if (target.position.y < transform.position.y && !neverDown)
            {
                animator.SetFloat("moveY", -1f);
                lookingAt = 2;                 //Bas
            }
            if (target.position.y > transform.position.y && !neverUp)
            {
                animator.SetFloat("moveY", 1f);
                lookingAt = 4;                 //Haut
            }
        }
    }

    void Attack(int lookingAt)
    {
        if (lookingAt == 1) //Droite
        {
            pushCorrupted1.SetActive(true);
            pushCorrupted2.SetActive(false);
            pushCorrupted3.SetActive(false);
            pushCorrupted4.SetActive(false);
        }
        else if (lookingAt == 2) //Bas
        {
            pushCorrupted2.SetActive(true);
            pushCorrupted1.SetActive(false);
            pushCorrupted3.SetActive(false);
            pushCorrupted4.SetActive(false);
        }
        else if (lookingAt == 3) // Gauche
        {
            pushCorrupted3.SetActive(true);
            pushCorrupted2.SetActive(false);
            pushCorrupted1.SetActive(false);
            pushCorrupted4.SetActive(false);
        }
        else if (lookingAt == 4) //Haut
        {
            pushCorrupted4.SetActive(true);
            pushCorrupted2.SetActive(false);
            pushCorrupted3.SetActive(false);
            pushCorrupted1.SetActive(false);
        }
    }

    void Dead()
    {
        animator.SetBool("isHurting", true);
    }
}
