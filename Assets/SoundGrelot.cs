using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGrelot : MonoBehaviour
{
    public AudioClip grelotSound;
    public AudioClip explosionSound;

    private AudioSource audioS;

    public void GrelotExplo()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.volume = 0.05f;
        audioS.PlayOneShot(grelotSound);
        audioS.PlayOneShot(explosionSound);
    }
}
