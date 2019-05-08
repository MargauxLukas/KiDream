using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickBehaviour : MonoBehaviour
{
    private bool isDream = true;

    private GameObject player;
    private ParticleSystem ps;
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

    private int lookingAt = 0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up

    void Start ()
    {
        player   = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        target = player.transform;
    }
	
	void Update ()
    {
        isDream = player.GetComponent<CharacterController>().isDream;

        if (isDream) { animator.SetBool("isDream",  true);}
        else         { animator.SetBool("isDream", false);}

        LookAt();
        Attack(lookingAt);
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
                animator.SetFloat("moveX", 1f);
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
}
