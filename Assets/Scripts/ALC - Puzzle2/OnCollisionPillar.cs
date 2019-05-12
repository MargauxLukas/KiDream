using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionPillar : MonoBehaviour
{
    public GameObject puzzleCollision;
    private Animator animator;

    public bool isActivated = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == puzzleCollision)
        {
            animator.SetBool("isChecked", true);
            isActivated = true;
        }
        else
        { return; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<OnCollisionPillar>().isActivated == true)
        {
            animator.SetBool("isChecked", true);
            isActivated = true;
        }
        else { return; }

    }
}
