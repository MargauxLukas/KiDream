using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourneDisqueTrigger : MonoBehaviour
{
    Animator musicAnim;

    CharacterController myPlayer;

    bool isPlaying = false;

    void Start()
    {
        musicAnim = GetComponentInParent<Animator>();
        myPlayer = FindObjectOfType<CharacterController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isPlaying == false)
        {
            musicAnim.SetBool("isTouched", true);

            isPlaying = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            musicAnim.SetBool("isTouched", false);

            isPlaying = false;
        }
    }
}
