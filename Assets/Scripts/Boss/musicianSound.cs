using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicianSound : MonoBehaviour
{
    public AudioClip musicianS;
    public AudioClip wrongKey;

    private AudioSource audioS;

    public void PlayMusicianSound()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.volume = 0.4f;
        audioS.PlayOneShot(musicianS);
    }

    public void StopMusicianSound()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.Stop();
        audioS.PlayOneShot(wrongKey);
        audioS.volume = 0.4f;
    }

}
