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

    public List<AudioClip> dreamClips = new List<AudioClip>();
    public List<AudioClip> nightmareClips = new List<AudioClip>();

    [SerializeField]
    private bool activeObject = false;
    public List<GameObject> ActivableObjects = new List<GameObject>();

    [SerializeField]
    private bool animate = false;
    public Animator objectToAnimate;
    public string name;

    [SerializeField]
    private bool disableCollider;
    public BoxCollider2D bc;

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

        //Debug.Log("HasCollide");
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
                    if(dreamClips.Count != 0)
                    {
                        acDream = dreamClips[Random.Range(0, dreamClips.Count)];
                    }
                    myAudioSource.PlayOneShot(acDream);
                    break;
                case false:
                    if(nightmareClips.Count != 0)
                    {
                        acNightmare = nightmareClips[Random.Range(0, nightmareClips.Count)];
                    }
                    myAudioSource.PlayOneShot(acNightmare);
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

        if(disableCollider == true)
        {
            bc.enabled = false;
        }

    }
}
