using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSound : MonoBehaviour
{

    public AudioClip portailOpen;

    private AudioSource audioS;

    public void portalOpenSound()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.PlayOneShot(portailOpen);
    }
}
