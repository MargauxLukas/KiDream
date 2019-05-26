using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioThemeManager : MonoBehaviour
{
    public AudioSource dreamTheme;
    public AudioSource nightmareTheme;

    AudioSource selectedTheme;
    AudioSource otherTheme;

    public CharacterController myPlayer;

	// Start
	void Start ()
    {
        PauseMenu.handleReturnedValue = 1f;
        selectedTheme = dreamTheme;
        otherTheme = nightmareTheme;
	}
	
	// Update
	void Update ()
    {
        ThemeChanger();
	}

    private void ThemeChanger()
    {
        if (!myPlayer.isKilled)
        {
            if (myPlayer.isDream == true && dreamTheme.volume > 0)
            {
                return;
            }
            else if (myPlayer.isDream == true && dreamTheme.volume == 0f)
            {
                selectedTheme = dreamTheme;
                otherTheme = nightmareTheme;
                StartCoroutine("FadeIn");
                StartCoroutine("FadeOut");
            }

            if (myPlayer.isDream == false && nightmareTheme.volume > 0)
            {
                return;
            }
            else if (myPlayer.isDream == false && nightmareTheme.volume == 0f)
            {
                selectedTheme = nightmareTheme;
                otherTheme = dreamTheme;
                StartCoroutine("FadeIn");
                StartCoroutine("FadeOut");
            }
        }
        else
        {
            selectedTheme.volume = 0f;
            otherTheme.volume = 0f;
        }
    }

    IEnumerator FadeIn()
    {
        for(float i = 0; i < 100 * PauseMenu.handleReturnedValue; i++)
        {
            selectedTheme.volume = selectedTheme.volume + 0.01f;
            yield return new WaitForSeconds(0.05f);

            if (selectedTheme.volume < 1f * PauseMenu.handleReturnedValue && selectedTheme.volume > 0.99f * PauseMenu.handleReturnedValue)
            {
                selectedTheme.volume = 1f*PauseMenu.handleReturnedValue;
            }
        }
    }

    IEnumerator FadeOut()
    {
        for (float i = 100; i > 0; i--)
        {
            otherTheme.volume = otherTheme.volume - 0.01f;
            yield return new WaitForSeconds(0.05f);

            if(otherTheme.volume > 0f * PauseMenu.handleReturnedValue && otherTheme.volume < 0.01f * PauseMenu.handleReturnedValue)
            {
                otherTheme.volume = 0f*PauseMenu.handleReturnedValue;
            }
        }
    }

}
