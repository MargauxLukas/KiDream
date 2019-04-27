using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public WaveType waveSelection;

    public List<ParticleSystem> WaveShooters = new List<ParticleSystem>();
    public List<Transform> WaveShootersTransform = new List<Transform>();

    public CharacterController myPlayer;

    public int selectionIndex = 0;
    private int count;

    // Start
    void Start ()
    {
        count = System.Enum.GetValues(typeof(WaveType)).Length - 1;
    }
	
	// Update
	void Update ()
    {

        WaveTypeSelector();

        foreach(ParticleSystem ps in WaveShooters)
        {
            if(ps == WaveShooters[selectionIndex])
            {
                ps.gameObject.SetActive(true);
            }
            else
            {
                ps.gameObject.SetActive(false);
            }
        }
	}

    public void WaveTypeSelector()
    {

        switch(myPlayer.reve)
        {
            case true:

                if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                {
                    if (selectionIndex == count / 2)
                    {
                        selectionIndex = 0;
                    }
                    else
                    {
                        selectionIndex++;
                    }
                }

                if (selectionIndex > WaveShooters.Count / 2)
                {
                    selectionIndex = selectionIndex - WaveShooters.Count / 2;
                }
                break;

            case false:

                if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                {
                    if (selectionIndex == count)
                    {
                        selectionIndex = 0;
                    }
                    else
                    {
                        selectionIndex++;
                    }
                }

                if (selectionIndex < WaveShooters.Count / 2)
                {
                    selectionIndex = selectionIndex + WaveShooters.Count / 2;
                }
                break;
        }

        switch (selectionIndex)
        {
            case 0:
                waveSelection = WaveType.Push;
                break;
            case 1:
                waveSelection = WaveType.Pull;
                break;
            case 2:
                waveSelection = WaveType.Activate;
                break;
            case 3:
                waveSelection = WaveType.PushCorruption;
                break;
            case 4:
                waveSelection = WaveType.PullCorruption;
                break;
            case 5:
                waveSelection = WaveType.ActivateCorruption;
                break;
        }

        /*
        Debug.Log(dreamSelection);
        Debug.Log(nightmareSelection);
        */
    }

    public void Push()
    {


    }

}

//selection = selection + listcount/2;
// - pour l'autre sens

public enum WaveType {Push, Pull, Activate, PushCorruption, PullCorruption, ActivateCorruption}