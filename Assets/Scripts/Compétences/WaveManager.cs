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
    public float pushCost;
    [Range(0, 100)]
    public float pullCost;
    [Range(0, 100)]
    public float activateCost;
    [Range(0, 100)]
    public float corruptedPushCost;
    [Range(0, 100)]
    public float corruptedPullCost;
    [Range(0, 100)]
    public float corruptedActivateCost;

    private int enumCount;

    [HideInInspector]
    public bool canDisable;
    private bool isPlayingCoroutine;

    public bool isRegenerating;

    public bool rightAxisInUse = false;

    public static float manaBarValue = 1f;

    // Start
    void Start ()
    {
        enumCount = System.Enum.GetValues(typeof(WaveType)).Length - 1;
    }
	
	// Update
	void Update ()
    {
        if(uiManager.manaBar.fillAmount < 1f && isRegenerating == false)
        {
            StartCoroutine("ManaRegenCoroutine");
        }
        else if(uiManager.manaBar.fillAmount >= 1f)
        {
            CancelInvoke();
            isRegenerating = false;
        }

        manaBarValue = uiManager.manaBar.fillAmount;

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

        switch (myPlayer.isDream)
        {
            case true:

                if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                {
                    if (selectionIndex == enumCount / 2)
                    {
                        selectionIndex = 0;
                    }
                    else
                    {
                        selectionIndex++;
                    }
                }

                if (selectionIndex >= WaveShooters.Count / 2)
                {
                    selectionIndex = selectionIndex - WaveShooters.Count / 2;
                }

                break;

            case false:

                if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                {
                    if (selectionIndex == enumCount)
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

        if (Input.GetAxisRaw("ShootParticles") != 0 && uiManager.manaBar.fillAmount != 0 && rightAxisInUse == false)
        {
            StopCoroutine("ChangingAbilityDisableDelay");
            StartCoroutine("ChangingAbilityDisableDelay");

            switch (selectionIndex)
                {
                    case 0:
                        waveSelection = WaveType.Push;
                        if (uiManager.manaBar.fillAmount >= pushCost / 100f)
                        {
                            uiManager.manaBar.fillAmount = uiManager.manaBar.fillAmount - pushCost/100f;
                            myPlayer.UseWave();
                        }
                        break;
                    case 1:
                        waveSelection = WaveType.Pull;
                        if (uiManager.manaBar.fillAmount >= pullCost / 100f)
                        {
                            uiManager.manaBar.fillAmount = uiManager.manaBar.fillAmount - pullCost/100f;
                            myPlayer.UseWave();
                        }
                        break;
                    case 2:
                        waveSelection = WaveType.Activate;
                        if (uiManager.manaBar.fillAmount >= activateCost / 100f)
                        {
                            uiManager.manaBar.fillAmount = uiManager.manaBar.fillAmount - activateCost/100f;
                            myPlayer.UseWave();
                        }
                        break;
                    case 3:
                        waveSelection = WaveType.PushCorruption;
                        if (uiManager.manaBar.fillAmount >= corruptedPushCost / 100f)
                        {
                            uiManager.manaBar.fillAmount = uiManager.manaBar.fillAmount - corruptedPushCost/100f;
                            myPlayer.UseWave();
                        }
                        break;
                    case 4:
                        waveSelection = WaveType.PullCorruption;
                        if (uiManager.manaBar.fillAmount >= corruptedPullCost / 100f)
                        {
                            uiManager.manaBar.fillAmount = uiManager.manaBar.fillAmount - corruptedPullCost/100f;
                            myPlayer.UseWave();
                        }
                        break;
                    case 5:
                        waveSelection = WaveType.ActivateCorruption;
                        if (uiManager.manaBar.fillAmount >= corruptedActivateCost / 100f)
                        {
                            uiManager.manaBar.fillAmount = uiManager.manaBar.fillAmount - corruptedActivateCost/100f;
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
        uiManager.manaBar.fillAmount = uiManager.manaBar.fillAmount + regenerationValue / 100f;
    }

    IEnumerator ChangingAbilityDisableDelay()
    {
        canDisable = false;
        yield return new WaitForSeconds(WaveShooters[selectionIndex].startLifetime);
        canDisable = true;
    }
}

public enum WaveType {Push, Pull, Activate, PushCorruption, PullCorruption, ActivateCorruption}