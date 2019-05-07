using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    Animator CheckpointAnim;
    public bool isChecked = false;

    void Start()
    {
        CheckpointAnim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isChecked == false)
        {
            CheckpointAnim.SetBool("isChecked", true);

            if(this.GetComponent<AudioSource>() != null && isChecked == false)
            {
                this.GetComponent<AudioSource>().Play();
            }

            isChecked = true;
        }
    }
}
