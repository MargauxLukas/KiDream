using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChange : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private bool isDream;

    private bool locked = false;

    private void Start()
    {
        player   = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(player.GetComponent<CharacterController>().isDream && locked == false)
        {
            animator.SetBool("isDream", true);
            locked = true;
        }
        else if (player.GetComponent<CharacterController>().isDream == false && locked == true)
        {
            animator.SetBool("isDream", false);
            locked = false;
        }
    }
}
