using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle01TrapAnimTrigger : MonoBehaviour
{
    Animator Anim;
    BoxCollider2D collider;
    public GameObject Cube;
    public bool isChecked = false;

    void Start()
    {
        Anim = GetComponentInParent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Cube && isChecked == false)
        {
            Anim.SetBool("isTouched", true);
            isChecked = true;
            collider.isTrigger = false;
            Destroy(this);
        }
    }
}
