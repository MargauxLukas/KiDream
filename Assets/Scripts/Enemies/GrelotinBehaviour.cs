using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrelotinBehaviour : MonoBehaviour
{
    [Header("Objet = New Target (Sympa pour scripter)")]
    public GameObject attachedObject; //Objet auquel il est attaché, il passera son temps à le pousser (Sympa pour scripté)
    [Header("PushCorrupted en enfant")]
    public GameObject pushCorrupted1;
    public GameObject pushCorrupted2;
    public GameObject pushCorrupted3;
    public GameObject pushCorrupted4;

    private bool isDream     =  true;
    private bool isAttacking = false;
    private bool canMove     = true;
    [Header("PushCorrupted power")]
    public float PushPower = 0.1f;

    private GameObject player;
    private Animator animator;
    private Transform  target;
    [Header("Speed Movement")]
    public float speedMoving = 0.5f;
    private int lookingAt    =    0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up

    void Start ()
    {
        pushCorrupted1.GetComponent<PushEffect>().forceX = PushPower;
        pushCorrupted2.GetComponent<PushEffect>().forceX = PushPower;
        pushCorrupted3.GetComponent<PushEffect>().forceX = PushPower;
        pushCorrupted4.GetComponent<PushEffect>().forceX = PushPower;
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();

        if (attachedObject != null)
        {
            target = attachedObject.transform;
        }
        else
        {
            target = player.transform;
        }
    }
	
	void Update ()
    {
        if (canMove){Move();}
        else { animator.SetBool("isMoving", false); }
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

    public void Attack(Collider2D hit)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("isAttacking");
            canMove = false;
            StartCoroutine(WaitAttack());
        }
        //Repousse Objet
    }

    IEnumerator WaitAttack()
    {
        if (lookingAt == 1) //Droite
        {
            pushCorrupted1.SetActive(true);
        }
        else if (lookingAt == 2) //Bas
        {
            pushCorrupted2.SetActive(true);
        }
        else if (lookingAt == 3) // Gauche
        {
            pushCorrupted3.SetActive(true);
        }
        else if (lookingAt == 4) //Haut
        {
            pushCorrupted4.SetActive(true);
        }
        yield return new WaitForSeconds(0.700f);
        animator.SetTrigger("isSleep");
        pushCorrupted1.SetActive(false);
        pushCorrupted2.SetActive(false);
        pushCorrupted3.SetActive(false);
        pushCorrupted4.SetActive(false);
        yield return new WaitForSeconds(2f);
        canMove = true;
        isAttacking = false;
    }
}
