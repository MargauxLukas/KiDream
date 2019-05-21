using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticleCollisionPlay : MonoBehaviour
{
    public CharacterController myPlayer;

    [SerializeField]
    private bool playParticleSystem = false;
    public ParticleSystem myParticleSystem;

    [SerializeField]
    private bool playAudio = false;
    public AudioSource myAudioSource;
    public AudioClip acDream;
    public AudioClip acNightmare;

    [SerializeField]
    private bool activeObject = false;
    public List<GameObject> ActivableObjects = new List<GameObject>();



    private void OnParticleCollision(GameObject other)
    {
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


    }
}
