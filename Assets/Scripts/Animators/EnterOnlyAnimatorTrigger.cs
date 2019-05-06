using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterOnlyAnimatorTrigger : MonoBehaviour
{
    Animator Anim;
    BoxCollider2D Collider;
    public bool isChecked = false;

    void Start()
    {
        Anim = GetComponentInParent<Animator>();
        Collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isChecked == false)
        {
            Anim.SetBool("isTouched", true);
            isChecked = true;
            Destroy(Collider);
            Destroy(this);
        }
    }
}
