using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{

    public Image dreamAbilitySelector;
    public Image nightmareAbilitySelector;
    public CharacterController myPlayer;

    public Image dreamCooldown;
    public Image nightmareCooldown;

    public float refreshRate;

    [SerializeField]
    private bool launchDreamCD = true;
    [SerializeField]
    private bool launchNightmareCD = true;


    public GPSAbility gpsAbility;
    public BAITAbility baitAbility;
    
	// Start
	void Start ()
    {

        dreamCooldown.fillAmount = 0;
        nightmareCooldown.fillAmount = 0;

    }
	
	// Update
	void Update ()
    {

        UIAbilityIndicator();
        AbilityCooldown();

    }


    public void UIAbilityIndicator()
    {
        if (myPlayer.reve == true)
        {
            nightmareAbilitySelector.enabled = false;
            dreamAbilitySelector.enabled = true;
        }
        else if (myPlayer.reve == false)
        {
            dreamAbilitySelector.enabled = false;
            nightmareAbilitySelector.enabled = true;
        }
    }


    public void AbilityCooldown()
    {
        if(gpsAbility.isLaunching == true && launchDreamCD == true)
        {
            dreamCooldown.fillAmount = 1;
            InvokeRepeating("DreamCooldown", 0, gpsAbility.gpsCooldownTime * (1 / (gpsAbility.gpsCooldownTime / refreshRate)));           
        }


        if (baitAbility.baitIsLaunched == true && launchNightmareCD == true)
        {
            nightmareCooldown.fillAmount = 1;
            InvokeRepeating("NightmareCooldown", 0, baitAbility.baitCooldownTime * (1 / (baitAbility.baitCooldownTime / refreshRate)));
        }
    }


    public void DreamCooldown()
    {
        launchDreamCD = false;
        dreamCooldown.fillAmount = dreamCooldown.fillAmount - 1 / (gpsAbility.gpsCooldownTime / refreshRate);


        if (dreamCooldown.fillAmount <= 0)
        {
            StartCoroutine(DelayResetDreamCD());
        }
    }


    public void NightmareCooldown()
    {
        launchNightmareCD = false;
        nightmareCooldown.fillAmount = nightmareCooldown.fillAmount - 1 / (baitAbility.baitCooldownTime / refreshRate);


        if (nightmareCooldown.fillAmount <= 0)
        {
            StartCoroutine(DelayResetNightmareCD());
        }

    }


    IEnumerator DelayResetDreamCD()
    {
        CancelInvoke("DreamCooldown");
        yield return new WaitForSeconds(0.1f);
        launchDreamCD = true;
    }

    IEnumerator DelayResetNightmareCD()
    {
        CancelInvoke("NightmareCooldown");
        yield return new WaitForSeconds(0.1f);
        launchNightmareCD = true;
    }
}
