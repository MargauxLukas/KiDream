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

    public GameObject drum;
    public GameObject trompette;
    public GameObject piano;
    public GameObject triangle;
    public GameObject chant;
    public GameObject violon;

    private Animator drumAnim;
    private Animator trompetteAnim;
    private Animator pianoAnim;
    private Animator triangleAnim;
    private Animator chantAnim;
    private Animator violonAnim;

    public AudioClip introHell;
    public AudioClip surpriseS;

    public AudioSource audioS;
    public AudioSource surpriseAS;

    private void Start()
    {
        drumAnim      = drum.GetComponent     <Animator>();
        trompetteAnim = trompette.GetComponent<Animator>();
        pianoAnim     = piano.GetComponent    <Animator>();
        triangleAnim  = triangle.GetComponent <Animator>();
        chantAnim     = chant.GetComponent    <Animator>();
        violonAnim    = violon.GetComponent<Animator>();
    }

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
        drumAnim.enabled = false;
        trompetteAnim.enabled = false;
        pianoAnim.enabled = false;
        triangleAnim.enabled = false;
        chantAnim.enabled = false;
        violonAnim.enabled = false;
    }

    public void Grelot7Sound()
    {
        audioS.volume = 0.3f;
        audioS.PlayOneShot(grelot7Sound);
    }

    public void HellSound()
    {
        audioS.volume = 0.3f;
        drumAnim.enabled = true;
        trompetteAnim.enabled = true;
        pianoAnim.enabled = true;
        triangleAnim.enabled = true;
        chantAnim.enabled = true;
        violonAnim.enabled = true;
        audioS.PlayOneShot(introHell);
    }
}
