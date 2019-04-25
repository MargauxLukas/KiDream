using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Key3 : MonoBehaviour
{

    public bool key3Done = false;

    public Key1 key1;
    public Key2 key2;
    public Key4 key4;

    public AudioSource keyAudio;
    public List<AudioClip> DongSoundsList = new List<AudioClip>();

    public WavePuzzleManager wavePuzzleManager;

    public List<Transform> Key3Bounds = new List<Transform>();

    // Start
    void Start()
    {

    }

    // Update
    void Update()
    {
        //BoundRight
        if (this.transform.position.x < Key3Bounds[0].position.x)
        {
            this.transform.position = new Vector2(Key3Bounds[0].position.x, this.transform.position.y);
        }

        //BoundLeft
        if (this.transform.position.x > Key3Bounds[1].position.x)
        {
            this.transform.position = new Vector2(Key3Bounds[1].position.x, this.transform.position.y);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        keyAudio.clip = DongSoundsList[Random.Range(0, DongSoundsList.Count)];
        keyAudio.Play();

        if (key1.key1Done == true && key2.key2Done == true && key3Done == false && key4.key4Done == false)
        {
            key3Done = true;
        }
        else
        {
            wavePuzzleManager.ResetPuzzle();
        }
    }


}