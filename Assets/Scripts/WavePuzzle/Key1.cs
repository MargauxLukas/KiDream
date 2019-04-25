using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Key1 : MonoBehaviour
{

    public bool key1Done = false;

    public Key2 key2;
    public Key3 key3;
    public Key4 key4;

    public AudioSource keyAudio;
    public List<AudioClip> DongSoundsList = new List<AudioClip>();

    public WavePuzzleManager wavePuzzleManager;

    public List<Transform> Key1Bounds = new List<Transform>();

    // Start
    void Start ()
    {

	}
	
	// Update
	void Update ()
    {
        //PitchTweaker();

        //BoundUp
        if (this.transform.position.y > Key1Bounds[0].position.y)
        {
            this.transform.position = new Vector2(this.transform.position.x, Key1Bounds[0].position.y);
        }

        //BoundDown
        if (this.transform.position.y < Key1Bounds[1].position.y)
        {
            this.transform.position = new Vector2(this.transform.position.x, Key1Bounds[1].position.y);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        keyAudio.clip = DongSoundsList[Random.Range(0, DongSoundsList.Count)];
        keyAudio.Play();

        if (key1Done == false && key2.key2Done == false && key3.key3Done == false && key4.key4Done == false)
        {
            key1Done = true;
        }
        else
        {
            wavePuzzleManager.ResetPuzzle();
        }
    }
}
