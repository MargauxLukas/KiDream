using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public WaveType waveSelection;

    public List<ParticleSystem> WaveShooters = new List<ParticleSystem>();
    public List<Transform> WaveShootersTransform = new List<Transform>();

    public CharacterController myPlayer;
    public UIManager uiManager;

    public int selectionIndex = 0;

    [Header("Abilities Cost")]
    public int regenerationValue;
    public float regenerationRate;
    [Range(0,100)]
    public int pushCost;
    [Range(0, 100)]
    public int pullCost;
    [Range(0, 100)]
    public int activateCost;
    [Range(0, 100)]
    public int corruptedPushCost;
    [Range(0, 100)]
    public int corruptedPullCost;
    [Range(0, 100)]
    public int corruptedActivateCost;

    private int enumCount;

    public bool canDisable;
    private bool isPlayingCoroutine;

    public bool isRegenerating;

    private bool rightAxisInUse = false;
    private bool leftAxisInUse = false;


    // Start
    void Start ()
    {
        enumCount = System.Enum.GetValues(typeof(WaveType)).Length - 1;
    }
	
	// Update
	void Update ()
    {
        if(uiManager.manaBar.value < uiManager.manaBar.maxValue && isRegenerating == false)
        {
            StartCoroutine("ManaRegenCoroutine");
        }
        else if(uiManager.manaBar.value >= uiManager.manaBar.maxValue)
        {
            CancelInvoke();
            isRegenerating = false;
        }      

        WaveTypeSelector();

        foreach(ParticleSystem ps in WaveShooters)
        {
            if(ps == WaveShooters[selectionIndex])
            {
                ps.gameObject.SetActive(true);
            }
            else if(ps != WaveShooters[selectionIndex] && canDisable == true)
            {
                ps.gameObject.SetActive(false);
            }
        }
	}

    public void WaveTypeSelector()
    {
        if (Input.GetAxisRaw("ShootParticles") == 0)
        {
            rightAxisInUse = false;
        }

        if (Input.GetAxisRaw("ChangeParticles") == 0)
        {
            leftAxisInUse = false;
        }

        switch (myPlayer.isDream)
        {
            case true:

                if (Input.GetAxisRaw("ChangeParticles") != 0 && leftAxisInUse == false)
                {
                    if (selectionIndex == enumCount / 2)
                    {
                        selectionIndex = 0;
                    }
                    else
                    {
                        selectionIndex++;
                    }

                    leftAxisInUse = true;
                }

                if (selectionIndex >= WaveShooters.Count / 2)
                {
                    selectionIndex = selectionIndex - WaveShooters.Count / 2;
                }

                break;

            case false:

                if (Input.GetAxisRaw("ChangeParticles") != 0 && leftAxisInUse == false)
                {
                    if (selectionIndex == enumCount)
                    {
                        selectionIndex = 0;
                    }
                    else
                    {
                        selectionIndex++;
                    }

                    leftAxisInUse = true;
                }

                if (selectionIndex < WaveShooters.Count / 2)
                {
                    selectionIndex = selectionIndex + WaveShooters.Count / 2;
                }

                break;
        }

        if (Input.GetAxisRaw("ShootParticles") != 0 && uiManager.manaBar.value != 0 && rightAxisInUse == false)
        {
            StopAllCoroutines();
            StartCoroutine("ChangingAbilityDisableDelay");

            switch (selectionIndex)
                {
                    case 0:
                        waveSelection = WaveType.Push;
                        if (uiManager.manaBar.value >= pushCost)
                        {
                            uiManager.manaBar.value = uiManager.manaBar.value - pushCost;
                            myPlayer.UseWave();
                        }
                        break;
                    case 1:
                        waveSelection = WaveType.Pull;
                        if (uiManager.manaBar.value >= pullCost)
                        {
                            uiManager.manaBar.value = uiManager.manaBar.value - pullCost;
                            myPlayer.UseWave();
                        }
                        break;
                    case 2:
                        waveSelection = WaveType.Activate;
                        if (uiManager.manaBar.value >= activateCost)
                        {
                            uiManager.manaBar.value = uiManager.manaBar.value - activateCost;
                            myPlayer.UseWave();
                        }
                        break;
                    case 3:
                        waveSelection = WaveType.PushCorruption;
                        if (uiManager.manaBar.value >= corruptedPushCost)
                        {
                            uiManager.manaBar.value = uiManager.manaBar.value - corruptedPushCost;
                            myPlayer.UseWave();
                        }
                        break;
                    case 4:
                        waveSelection = WaveType.PullCorruption;
                        if (uiManager.manaBar.value >= corruptedPullCost)
                        {
                            uiManager.manaBar.value = uiManager.manaBar.value - corruptedPullCost;
                            myPlayer.UseWave();
                        }
                        break;
                    case 5:
                        waveSelection = WaveType.ActivateCorruption;
                        if (uiManager.manaBar.value >= corruptedActivateCost)
                        {
                            uiManager.manaBar.value = uiManager.manaBar.value - corruptedActivateCost;
                            myPlayer.UseWave();
                        }
                        break;
                }
                rightAxisInUse = true;
        }
    }

    IEnumerator ManaRegenCoroutine()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(1);
        InvokeRepeating("ManaRegen", 0, regenerationRate);
    }

    public void ManaRegen()
    {
        uiManager.manaBar.value = uiManager.manaBar.value + regenerationValue;
    }

    IEnumerator ChangingAbilityDisableDelay()
    {
        canDisable = false;
        yield return new WaitForSeconds(WaveShooters[selectionIndex].startLifetime);
        canDisable = true;
    }
}

public enum WaveType {Push, Pull, Activate, PushCorruption, PullCorruption, ActivateCorruption}