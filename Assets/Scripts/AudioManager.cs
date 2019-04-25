using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixer myMixer;
    public CharacterController myPlayer;

    float mainPitch = 100f;
    float pitchValue = 1f;
    float FFTSize = 1024f;
    float overlap = 4f;

    [SerializeField] float reachedMainPitchDream;
    [SerializeField] float reachedPitchDream;
    [SerializeField] float reachedFFTDream;
    [SerializeField] float reachedOverlapDream;

    [SerializeField] float reachedMainPitchNightmare;
    [SerializeField] float reachedPitchNightmare;
    [SerializeField] float reachedFFTNightmare;
    [SerializeField] float reachedOverlapNightmare;




    // Start
    void Start ()
    {
		


	}
	
	// Update
	void Update ()
    {
		
        if(Input.GetKeyDown(KeyCode.T) && myPlayer.reve == false)
        {
            CancelInvoke();
            InvokeRepeating("SwitchToNightmareSong", 0.25f, 0.25f);
            StartCoroutine(DelayToStopInvoke());
        }
        else if (Input.GetKeyDown(KeyCode.T) && myPlayer.reve == true)
        {
            CancelInvoke();
            InvokeRepeating("SwitchToDreamSong", 0.25f, 0.25f);
            StartCoroutine(DelayToStopInvoke());
        }

    }


    public void SwitchToNightmareSong()
    {

        myMixer.GetFloat("Main Pitch", out mainPitch);
        if(mainPitch != reachedMainPitchNightmare)
        {
            myMixer.SetFloat("Main Pitch", mainPitch - 0.01f);
        }

        myMixer.GetFloat("Pitch", out pitchValue);
        if (mainPitch != reachedMainPitchNightmare)
        {
            myMixer.SetFloat("Pitch", pitchValue - 0.05f);
        }

        myMixer.GetFloat("FFT size", out FFTSize);
        if (mainPitch != reachedMainPitchNightmare)
        {
            myMixer.SetFloat("FFT size", FFTSize + 128f);
        }

        myMixer.GetFloat("Overlap", out overlap);
        if (mainPitch != reachedMainPitchNightmare)
        {
            myMixer.SetFloat("Overlap", overlap +0.5f);
        }
    }

    public void SwitchToDreamSong()
    {

        myMixer.GetFloat("Main Pitch", out mainPitch);
        if (mainPitch != reachedMainPitchDream)
        {
            myMixer.SetFloat("Main Pitch", mainPitch + 0.01f);
        }

        myMixer.GetFloat("Pitch", out pitchValue);
        if (mainPitch != reachedMainPitchDream)
        {
            myMixer.SetFloat("Pitch", pitchValue + 0.05f);
        }

        myMixer.GetFloat("FFT size", out FFTSize);
        if (mainPitch != reachedMainPitchDream)
        {
            myMixer.SetFloat("FFT size", FFTSize - 128f);
        }

        myMixer.GetFloat("Overlap", out overlap);
        if (mainPitch != reachedMainPitchDream)
        {
            myMixer.SetFloat("Overlap", overlap - 0.5f);
        }
    }


    IEnumerator DelayToStopInvoke()
    {
        yield return new WaitForSeconds(10);
        CancelInvoke();
    }

}
