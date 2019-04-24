using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticleCollisionPlay : MonoBehaviour
{
    [SerializeField]
    private bool playParticleSystem = false;
    public ParticleSystem myParticleSystem;

    [SerializeField]
    private bool playAudio = false;
    public AudioSource myAudioSource;

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
            myAudioSource.Play();
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
