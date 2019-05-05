using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterOnlyAnimatorTrigger : MonoBehaviour
{
    Animator Anim;
    public bool isChecked = false;

    void Start()
    {
        Anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(Anim);
        if (collision.tag == "Player" && isChecked == false)
        {
            Anim.SetBool("isTouched", true);
            isChecked = true;
        }
    }
}
