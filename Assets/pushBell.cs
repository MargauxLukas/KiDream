using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushBell : MonoBehaviour
{
    public GameObject pushCorrupted;

    public int secondsBeetweenWaves;

    private float timer;
    private int seconds = 0;
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > timer + 1) //Timer
        {
            timer = Time.time;
            seconds++;
        }

        if (seconds == secondsBeetweenWaves)
        {
            pushCorrupted.SetActive(true);

        }
        else if (seconds == secondsBeetweenWaves + 1)
        {
            seconds = 0;
        }
        else
        {
            pushCorrupted.SetActive(false);
        }
    }
}
