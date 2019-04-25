using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    public AudioMixerSnapshot unpausedAMS;
    public AudioMixerSnapshot pausedAMS;

	// Start
	void Start ()
    {
		
	}
	
	// Update
	void Update ()
    {
		
        if(Input.GetKeyDown(KeyCode.Space))
        {

            ChangeVolume();

        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            ChangeSnapshot();

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {


        }

        if (Input.GetKeyDown(KeyCode.E))
        {

        }

    }

    void ChangeVolume()
    {
        audioMixer.SetFloat("MasterVolume", -80.0f);
    }

    void ChangeSnapshot()
    {
        unpausedAMS.TransitionTo(0.1f);
    }



}
