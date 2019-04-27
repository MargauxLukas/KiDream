using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public DreamWaveType dreamSelection;
    public NightmareWaveType nightmareSelection;

    public List<ParticleSystem> WaveShooters = new List<ParticleSystem>();
    public List<Transform> WaveShootersTransform = new List<Transform>();

    public int selectionIndex = 0;
    private int count;

    // Start
    void Start ()
    {
        count = System.Enum.GetValues(typeof(DreamWaveType)).Length - 1;
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

        switch (selectionIndex)
        {
            case 0:
                dreamSelection = DreamWaveType.Push;
                nightmareSelection = NightmareWaveType.PushCorruption;
                break;
            case 1:
                dreamSelection = DreamWaveType.Pull;
                nightmareSelection = NightmareWaveType.PullCorruption;
                break;
            case 2:
                dreamSelection = DreamWaveType.Activate;
                nightmareSelection = NightmareWaveType.ActivateCorruption;
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

public enum DreamWaveType {Push, Pull, Activate}
public enum NightmareWaveType {PushCorruption, PullCorruption, ActivateCorruption}