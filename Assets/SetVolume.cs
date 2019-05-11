using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour {

    AudioSource[] myAudios;

    private float hasChanged;
    private float initialVolume;

    // Use this for initialization
    void Start ()
    {
        myAudios = this.GetComponents<AudioSource>();

        foreach (AudioSource auSo in myAudios)
        {
            Debug.Log(auSo.volume + " x " + PauseMenu.handleReturnedValue);
            initialVolume = auSo.volume;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {


        if(hasChanged != PauseMenu.handleReturnedValue)
        {
            hasChanged = PauseMenu.handleReturnedValue;

            foreach (AudioSource auSo in myAudios)
            {
                auSo.volume = initialVolume * PauseMenu.handleReturnedValue;
            }
        }
	}
}
