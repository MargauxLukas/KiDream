using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySoundsWall : MonoBehaviour
{
    public AudioClip fallingWall;

    private AudioSource audioS;

    public void fallingWallSound()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        //audioS.clip = fallingWall;
        audioS.PlayOneShot(fallingWall);
    }
}
