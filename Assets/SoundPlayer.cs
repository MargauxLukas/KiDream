using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource thisAudioSource;
    CharacterController myPlayer;

    public AudioClip dreamSound;
    public AudioClip nightmareSound;

    public bool stopWhenExit;

    [Range(0, 3)]
    public float dreamDelay;
    [Range(0,3)]
    public float nightmareDelay;

    public bool playFromList;
    public bool loopWhenIn;
    [Range(0,5)]
    public float loopingRate;

    public List<AudioClip> dreamAudioList = new List<AudioClip>();
    public List<AudioClip> nightmareAudioList = new List<AudioClip>();

    Collider2D myCollider;


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
        myCollider = collision;

        if(loopWhenIn == false)
        {
            StartCoroutine(SoundDelay(myCollider));
        }
        else if(loopWhenIn == true)
        {
            StartCoroutine(SoundDelay(myCollider));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && stopWhenExit == true)
        {
            this.thisAudioSource.Stop();
        }

        StopAllCoroutines();
    }

    IEnumerator SoundDelay(Collider2D c2d)
    {
        if (c2d.tag == "Player")
        {
            if(playFromList == false)
            {
                switch (myPlayer.isDream)
                {
                    case true:
                        //Joue le son rêve définit dans l'éditeur
                        yield return new WaitForSeconds(dreamDelay);
                        thisAudioSource.PlayOneShot(dreamSound);
                        break;

                    case false:
                        //Joue le son cauchemar définit dans l'éditeur
                        yield return new WaitForSeconds(nightmareDelay);
                        thisAudioSource.PlayOneShot(nightmareSound);
                        break;
                }
            }
            else if(playFromList == true)
            {
                switch (myPlayer.isDream)
                {
                    case true:

                        //Joue un son random de la liste de sons rêve
                        yield return new WaitForSeconds(dreamDelay);
                        dreamSound = dreamAudioList[Random.Range(0, dreamAudioList.Count - 1)];
                        thisAudioSource.PlayOneShot(dreamSound);

                        //Relance la Coroutine avec un délai à définir
                        if (loopWhenIn == true)
                        {
                            yield return new WaitForSeconds(loopingRate);
                            StartCoroutine(SoundDelay(myCollider));
                        }
                        break;
                    case false:

                        //Joue un son random de la liste de sons cauchemar
                        yield return new WaitForSeconds(nightmareDelay);
                        nightmareSound = nightmareAudioList[Random.Range(0, dreamAudioList.Count - 1)];
                        thisAudioSource.PlayOneShot(nightmareSound);

                        //Relance la Coroutine avec un délai à définir
                        if (loopWhenIn == true)
                        {
                            yield return new WaitForSeconds(loopingRate);
                            StartCoroutine(SoundDelay(myCollider));
                        }
                        break;
                }

            }
        }
    }
}
