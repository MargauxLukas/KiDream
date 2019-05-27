using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveSound : MonoBehaviour {

    public AudioClip saveSounds;

    private AudioSource audioS;

    public void PlaySaveSound()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.PlayOneShot(saveSounds);
    }
}
