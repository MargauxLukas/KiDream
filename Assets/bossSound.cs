using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class bossSound : MonoBehaviour
{
    public AudioClip grelot1Sound;
    public AudioClip grelot2Sound;
    public AudioClip grelot3Sound;
    public AudioClip grelot4Sound;
    public AudioClip grelot5Sound;
    public AudioClip grelot6Sound;
    public AudioClip grelot7Sound;
    public AudioClip introHell;
    public AudioClip surpriseS;

    public AudioSource audioS;
    public AudioSource surpriseAS;

    public void Grelot1Sound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(grelot1Sound);
    }

    public void Grelot2Sound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(grelot2Sound);
    }

    public void Grelot3Sound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(grelot3Sound);
    }

    public void Grelot4Sound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(grelot4Sound);
    }

    public void Grelot5Sound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(grelot5Sound);
    }

    public void Grelot6Sound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(grelot6Sound);
    }

    public void Grelot7Sound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(grelot7Sound);
    }

    public void HellSound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(introHell);
    }
}
