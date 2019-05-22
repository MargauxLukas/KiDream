using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject Player;
    public GameObject playerDeath;
    private Animator pAnimator;
    private Animator animator;

	void Start ()
    {
        animator = GetComponent<Animator>();
        pAnimator = playerDeath.GetComponent<Animator>();
	}
	
	public void deathScreen()
    {
        animator.SetBool("isDead", true);
        pAnimator.SetBool("isDead", true);
        pAnimator.SetBool("isDream", Player.GetComponent<CharacterController>().isDream);
    }
}
