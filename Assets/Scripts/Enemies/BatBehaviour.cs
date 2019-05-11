using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BatBehaviour : MonoBehaviour
{
    public GameObject    player;
    public GameObject    mySelf;
    public GameObject      cone;
    private ReactionToWave  rtw;
    private WaveManager      wm;
    private Animator   animator;
    private Transform    target;

    private RaycastHit2D hit;
    private Collider2D playerCollider;
    private Collider2D mySelfCollider;
    private Collider2D playerConeCollider;

    [Header("Move : ")]
    public bool canMove = false;
    public float speedMoving = 0.5f;

    [Header("Attack (A besoin de pouvoir bouger) : ")]
    public bool canAttack  = false ;

    [Header("Protect : ")]
    public bool alwaysProtect = false;
    public bool isClever      = false;  //Se protège lorsque le joueur tire (Si le mob voit pas le joueur tirer, il se protege pas (Rebond)

    private bool onceBool     = true ;
    private bool isAttacking  = false;
    private bool playerAttack = false;
    private bool isDream      = true;

    public float xDifference;
    public float yDifference;
    private float timer;
    private int seconds = 0;
    private int lookingAt = 0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up

    void Start()
    {
        playerCollider =     player.GetComponent<Collider2D>();
        mySelfCollider = gameObject.GetComponent<Collider2D>();
        playerConeCollider = cone.GetComponentInChildren<Collider2D>();
        animator = GetComponent<Animator>();
        target   = player.transform;
        rtw      = GetComponent<ReactionToWave>();
        wm       = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }

    void Update()
    {
        hit = Physics2D.Linecast(new Vector2(transform.position.x+1f, transform.position.y), target.transform.position);

        isDream = player.GetComponent<CharacterController>().isDream;

        if (isDream) { animator.SetBool("isDream",  true); }
        else         { animator.SetBool("isDream", false); }

        if (Time.time > timer + 1) //Timer
        {
            timer = Time.time;
            seconds++;
        }

        if (seconds == 5)
        {
            animator.SetBool("isProtectingOver", true);
            playerAttack = false;

            if (!rtw.whoCanShootMe.Contains(player))
            {
                rtw.whoCanShootMe.Add(player);
            }      
        }

        LookAt();
        if (canMove)
        {
            Move();
        }
        if(canAttack && !isAttacking)
        {
            if      (xDifference <= 2f && (lookingAt == 1 || lookingAt == 3)) { Attack(); }
            else if (yDifference <= 2f && (lookingAt == 2 || lookingAt == 4)) { Attack(); }
        }
        if (alwaysProtect && onceBool)
        {
            AlwaysProtect();
            onceBool = false;
        }
        if(isClever)
        {
            CleverFunction();
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speedMoving * Time.deltaTime);
    }

    void CleverFunction()
    {
        if (hit.collider != null && hit.collider != playerCollider && hit.collider != mySelfCollider && hit.collider != playerConeCollider)
        {
            Debug.Log("Je ne vois pas le joueur");
        }
        else
        {
            Debug.Log("Je vois le joueur");
            if (wm.rightAxisInUse)
            {
                Protect();
            }
        }
    }

    void LookAt()
    {
        xDifference = Mathf.Abs(transform.position.x - target.position.x);
        yDifference = Mathf.Abs(transform.position.y - target.position.y);

        if (xDifference > yDifference)
        {
            animator.SetFloat("moveY", 0f);

            if (target.position.x > transform.position.x)
            {
                animator.SetFloat("moveX", 1f);
                lookingAt = 1;                 //Droite
            }
            if (target.position.x < transform.position.x)
            {
                animator.SetFloat("moveX", -1f);
                lookingAt = 3;                 //Gauche
            }
        }
        else if (xDifference < yDifference)
        {
            animator.SetFloat("moveX", 0f);

            if (target.position.y < transform.position.y)
            {
                animator.SetFloat("moveY", -1f);
                lookingAt = 2;                 //Bas
            }
            if (target.position.y > transform.position.y)
            {
                animator.SetFloat("moveY", 1f);
                lookingAt = 4;                 //Haut
            }
        }
    }

    void Attack()
    {
        isAttacking = true;
        speedMoving = 1f;
        animator.SetBool("isAttacking", true);
        StartCoroutine(WaitAttack());
    }

    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(2f);
        speedMoving = 0.5f;
        isAttacking = false;
    }

    void AlwaysProtect()
    {
        rtw.whoCanShootMe.Clear();
        animator.SetTrigger("isProtecting");
    }

    void Protect()
    {
        seconds = 0;
        animator.SetBool("isProtectingOver", false);
        if (!playerAttack)
        {
            rtw.whoCanShootMe.Clear();
            animator.SetTrigger("isProtecting");
            playerAttack = true;
        }
    }

    /*IEnumerator ProtectWait()
    {
        seconds = 0;
        animator.SetBool("isProtectingOver", false);
        if (!playerAttack)
        {
            rtw.whoCanShootMe.Clear();
            animator.SetTrigger("isProtecting");
            playerAttack = true;
        }
        
        yield return new WaitForSeconds(5f);

        if (seconds == 5)
        {
            animator.SetBool("isProtectingOver", true);
            playerAttack = false;
            rtw.whoCanShootMe.Add(player);
        }
    }*/

    void Dead()
    {
        animator.SetBool("isHurting", true);
    }
}
