using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySounds : MonoBehaviour
{
    public AudioClip fallingWall;

    public AudioSource audioS;

    public void fallingWallSound()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        //audioS.PlayOneShot(fallingWall);
        audioS.clip = fallingWall;
        audioS.Play();
    }
}
