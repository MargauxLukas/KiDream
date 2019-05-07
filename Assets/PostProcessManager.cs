using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{

    public PostProcessVolume mainCamPP;
    public CharacterController myPlayer;

    public PostProcessVolume secondPP;

    public float autoExpoStep;
    public float autoExpoHighAim;
    public float autoExpoLowAim;

    public float rate;

    bool isLaunched = false;

	// Start
	void Start ()
    {
        mainCamPP.profile.name = "DreamPP";
        Debug.Log(mainCamPP.profile.name);
	}
	
	// Update
	void Update ()
    {
        if(mainCamPP.profile.name.Contains("Dream") && myPlayer.isDream == false && isLaunched == false)
        {
            StartCoroutine("PP_Transition");
        }

    }

    IEnumerator PP_Transition()
    {
        isLaunched = true;

        AutoExposure autoExpo = mainCamPP.profile.GetSetting<AutoExposure>();

        for(autoExpo.keyValue.value = autoExpo.keyValue.value; autoExpo.keyValue.value <= autoExpoHighAim; autoExpo.keyValue.value = autoExpo.keyValue.value + autoExpoStep)
        {
            yield return new WaitForSeconds(rate);
        }

        if(mainCamPP.name.Contains("Dream") && autoExpo.keyValue.value == autoExpoHighAim)
        {
            PostProcessVolume temp = mainCamPP;

            mainCamPP.profile = secondPP.profile;
            secondPP.profile = temp.profile;

            mainCamPP.profile.name = "NightmarePP";
        }
        else if(mainCamPP.name.Contains("Nightmare") && autoExpo.keyValue.value == autoExpoHighAim)
        {
            PostProcessVolume temp = mainCamPP;

            mainCamPP.profile = secondPP.profile;
            secondPP.profile = temp.profile;

            mainCamPP.profile.name = "DreamPP";
        }



    }
}
