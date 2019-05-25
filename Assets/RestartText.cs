using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartText : MonoBehaviour
{
    private Animator animator;

	void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
    public void TextAppear()
    {
        animator.SetBool("isDead", true);
    }
}
