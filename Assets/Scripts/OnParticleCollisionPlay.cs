using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticleCollisionPlay : MonoBehaviour
{
    CharacterController myPlayer;

    [SerializeField]
    private bool playParticleSystem = false;
    public ParticleSystem myParticleSystem;

    [SerializeField]
    private bool playAudio = false;

    AudioSource myAudioSource;

    public AudioClip acDream;
    public AudioClip acNightmare;

    [SerializeField]
    private bool activeObject = false;
    public List<GameObject> ActivableObjects = new List<GameObject>();

    [SerializeField]
    private bool animate = false;
    public Animator objectToAnimate;
    public string name;

    void Start()
    {
        myPlayer = FindObjectOfType<CharacterController>();

        if(this.GetComponent<AudioSource>() != null)
        {
            myAudioSource = this.GetComponent<AudioSource>();
        }
    }


    private void OnParticleCollision(GameObject other)
    {

        Debug.Log("HasCollide");
        if(playParticleSystem == true)
        {
            myParticleSystem = GetComponent<ParticleSystem>();
            myParticleSystem.Emit(1);
        }
        
        if(playAudio == true)
        {
            switch(myPlayer.isDream)
            {
                case true:
                    myAudioSource.clip = acDream;
                    myAudioSource.Play();
                    break;
                case false:
                    myAudioSource.clip = acNightmare;
                    myAudioSource.Play();
                    break;
            }

        }

        if(activeObject == true)
        {
            foreach (GameObject go in ActivableObjects)
            {
                go.SetActive(true);
            }
        }

        if(animate == true)
        {
            objectToAnimate.SetBool(name, true);
        }


    }
}
