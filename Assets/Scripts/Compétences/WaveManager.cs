using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public DreamWaveType dreamSelection;
    public NightmareWaveType nightmareSelection;

    public int selectionIndex = 0;

	// Start
	void Start () {
		
	}
	
	// Update
	void Update ()
    {
        WaveTypeSelector();
	}

    public void WaveTypeSelector()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            if (selectionIndex == 2)
            {
                selectionIndex = 0;
            }
            else
            {
                selectionIndex++;
                selectionIndex = Mathf.Clamp(selectionIndex, 0, 2);
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

}

public enum DreamWaveType {Push, Pull, Activate}
public enum NightmareWaveType {PushCorruption, PullCorruption, ActivateCorruption}