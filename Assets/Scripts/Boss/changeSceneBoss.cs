using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class changeSceneBoss : MonoBehaviour
{
    private string level = "ALC_Boss";
    public AudioClip windS;
    private AudioSource audioS;

    public void windSound()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.PlayOneShot(windS);
    }

	public void loadlevel()
    {
        SceneManager.LoadScene(level);
    }
}
