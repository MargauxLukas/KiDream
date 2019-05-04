using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourneDisqueTrigger : MonoBehaviour
{
    Animator musicAnim;

    void Start()
    {
        musicAnim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(musicAnim);
        if (collision.tag == "Player")
        {
            musicAnim.SetBool("isTouched", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            musicAnim.SetBool("isTouched", false);
        }
    }
}
