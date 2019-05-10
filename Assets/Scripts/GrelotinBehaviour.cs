using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrelotinBehaviour : MonoBehaviour
{
    private bool isDream     =  true;
    private bool isAttacking = false;

    private GameObject player;
    private Animator animator;
    private Transform  target;

    public float speedMoving = 0.5f;
    private int lookingAt    =    0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up

    void Start ()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        target = player.transform;
    }
	
	void Update ()
    {
        Move();
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
}
