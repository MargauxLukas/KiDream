using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource thisAudioSource;
    CharacterController myPlayer;

    public AudioClip dreamSound;
    public AudioClip nightmareSound;

    [Range(0, 3)]
    public float dreamDelay;
    [Range(0,3)]
    public float nightmareDelay;

	// Start
	void Start ()
    {
        thisAudioSource = this.GetComponent<AudioSource>();
        myPlayer = FindObjectOfType<CharacterController>();
	}
	
	// Update
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SoundDelay(collision));
    }

    IEnumerator SoundDelay(Collider2D c2d)
    {
        if (c2d.tag == "Player")
        {
            switch (myPlayer.isDream)
            {
                case true:
                    yield return new WaitForSeconds(dreamDelay);
                    thisAudioSource.PlayOneShot(dreamSound);
                    break;
                case false:
                    yield return new WaitForSeconds(nightmareDelay);
                    thisAudioSource.PlayOneShot(nightmareSound);
                    break;
            }
        }
    }
}
