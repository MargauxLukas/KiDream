using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle01TrapAnimTrigger : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    BoxCollider2D collider;
    public GameObject Cube;
    public bool isChecked = false;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Cube && isChecked == false)
        {
            anim1.SetTrigger("isTouched");
            anim2.SetTrigger("isTouched");
            isChecked = true;
            collider.isTrigger = false;
            Destroy(this);
        }
    }
}
