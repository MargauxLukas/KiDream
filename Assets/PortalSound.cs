using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSound : MonoBehaviour
{

    public AudioClip saveSound;

    private AudioSource audioS;

    public void PlaySaveSound()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.PlayOneShot(saveSound);
    }
}
