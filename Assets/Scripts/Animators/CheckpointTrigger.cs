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
        Debug.Log(CheckpointAnim);
        if (collision.tag == "Player" && isChecked == false)
        {
            CheckpointAnim.SetBool("isChecked", true);
            isChecked = true;
        }
    }
}
