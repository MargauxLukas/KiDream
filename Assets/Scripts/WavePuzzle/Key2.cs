using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Key2 : MonoBehaviour
{

    public bool key2Done = false;

    public Key1 key1;
    public Key3 key3;
    public Key4 key4;

    public AudioSource keyAudio;
    public List<AudioClip> DongSoundsList = new List<AudioClip>();

    public WavePuzzleManager wavePuzzleManager;

    public List<Transform> Key2Bounds = new List<Transform>();

    // Start
    void Start()
    {

    }

    // Update
    void Update()
    {

        //BoundUp
        if (this.transform.position.y > Key2Bounds[0].position.y)
        {
            this.transform.position = new Vector2(this.transform.position.x, Key2Bounds[0].position.y);
        }

        //BoundDown
        if (this.transform.position.y < Key2Bounds[1].position.y)
        {
            this.transform.position = new Vector2(this.transform.position.x, Key2Bounds[1].position.y);
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        keyAudio.clip = DongSoundsList[Random.Range(0, DongSoundsList.Count)];
        keyAudio.Play();

        if (key1.key1Done == true && key2Done == false && key3.key3Done == false && key4.key4Done == false)
        {
            key2Done = true;
        }
        else
        {
            wavePuzzleManager.ResetPuzzle();
        }
    }


}
