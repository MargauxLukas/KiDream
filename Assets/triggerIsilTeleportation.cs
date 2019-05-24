using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerIsilTeleportation : MonoBehaviour
{
    public GameObject isil;
    private Animator animator; 

	void Start ()
    {
        animator = isil.GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("CollisionDetected");
            animator.SetBool("isMuted", false);
        }
    }
}
