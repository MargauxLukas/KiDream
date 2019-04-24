using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{

    public Camera myCam;
    public AudioSource enteringAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enteringAudio.Play();
            myCam.orthographicSize = 1.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            myCam.orthographicSize = 1;
        }
    }
}
