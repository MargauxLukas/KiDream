using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterOnlyAnimatorTrigger : MonoBehaviour
{
    public GameObject gameObject;
    private Animator anim;
    BoxCollider2D Collider;
    public bool isChecked = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        Collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isChecked == false)
        {
            anim.SetBool("isTouched", true);
            isChecked = true;
            Destroy(Collider);
            Destroy(this);
        }
    }
}
