using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChange : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private bool isDream;

    private void Start()
    {
        player   = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(player.GetComponent<CharacterController>().isDream)
        {
            animator.SetBool("isDream", true);
        }
        else
        {
            animator.SetBool("isDream", false);
        }
    }
}
