using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerIsilTeleportation : MonoBehaviour
{
    public GameObject isil;
    private Animator animator;

    public bool triggerByName;
    public string triggerName;

	void Start ()
    {
        animator = isil.GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(triggerByName == false && collision.tag == "Player")
        {
            animator.SetBool("isMuted", false);
            GetComponent<Collider2D>().enabled = false;
        }
        else if(triggerByName == true && collision.gameObject.name == triggerName)
        {
            animator.SetBool("isMuted", false);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
