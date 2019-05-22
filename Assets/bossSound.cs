using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class bossSound : MonoBehaviour
{
    public AudioClip grelotSound;
    public AudioClip introHell;
    public AudioClip surpriseS;

    public AudioSource audioS;
    public AudioSource surpriseAS;

    public void GrelotSound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(grelotSound);
    }

    public void HellSound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(introHell);
    }
}
