using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    private bool isDream = true;

    private GameObject   player;
    private ReactionToWave  rtw;
    private Animator   animator;
    private Transform    target;

    [Header("Move : ")]
    public bool canMove = false;
    public float speedMoving = 0.5f;

    [Header("Attack : ")]
    public bool canAttack  = false ;

    [Header("Protect : ")]
    public bool alwaysProtect = false;

    private bool onceBool = true;

    public float xDifference;
    public float yDifference;

    private int lookingAt = 0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up

    void Start()
    {
        player   = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        target   = player.transform;
        rtw      = GetComponent<ReactionToWave>();
    }

    void Update()
    {
        isDream = player.GetComponent<CharacterController>().isDream;

        if (isDream) { animator.SetBool("isDream",  true); }
        else         { animator.SetBool("isDream", false); }

        LookAt();
        if (canMove)
        {
            Move();
        }
        if(canAttack)
        {
            if      (xDifference <= 2f && (lookingAt == 1 || lookingAt == 3))
            { Attack(); }
            else if (yDifference <= 2f && (lookingAt == 2 || lookingAt == 4))
            { Attack(); }
            else
            { speedMoving = 0.5f; }
        }
        if (alwaysProtect && onceBool)
        {
            AlwaysProtect();
            onceBool = false;
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speedMoving * Time.deltaTime);
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
        speedMoving = 1f;
        animator.SetBool("isAttacking", true);
    }

    void AlwaysProtect()
    {
        rtw.whoCanShootMe.Clear();
        animator.SetTrigger("isProtecting");
    }

    void Protect()
    {
        StartCoroutine(ProtectWait());
    }

    IEnumerator ProtectWait()
    {
        rtw.whoCanShootMe.Clear();
        animator.SetTrigger("isProtecting");
        yield return new WaitForSeconds(5f);
        animator.SetBool("isProtectingOver", true);
        rtw.whoCanShootMe.Add(player);
    }

    void Dead()
    {
        animator.SetBool("isHurting", true);
    }
}
