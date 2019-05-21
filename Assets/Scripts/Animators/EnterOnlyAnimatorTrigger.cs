﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterOnlyAnimatorTrigger : MonoBehaviour
{
    public GameObject gameObject;
    private Animator anim;
    BoxCollider2D Collider;
    public bool isChecked = false;

    private bool isilIsDescendu = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        Collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(isilIsDescendu)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1.285f, -1.8f), 9f*Time.fixedDeltaTime);

            if (gameObject.transform.position == new Vector3(-1.285f, -1.8f))
            {
                isChecked = true;
                Destroy(Collider);
                Destroy(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isChecked == false)
        {
            anim.SetBool("isTouched", true);
            isilIsDescendu = true;
        }
    }
}
