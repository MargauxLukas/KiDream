using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{

    public PostProcessVolume mainCamPP;
    public CharacterController myPlayer;

    AutoExposure autoExpo;
    Vignette vignette;
    Grain grain;
    ColorGrading colorGrading; //red 140 contrast 20 vignette 0.21 chromatic abberation active NIGHTMARE  //green 110 red 115 contrast 10
    ChromaticAberration cA;

    public float autoExpoStep;
    public float autoExpoHighAim;
    public float autoExpoLowAim;

    public float rate;

    public int position = 1;

    public bool isLaunched;

	// Start
	void Start ()
    {
        vignette = mainCamPP.profile.GetSetting<Vignette>();
        grain = mainCamPP.profile.GetSetting<Grain>();
        colorGrading = mainCamPP.profile.GetSetting<ColorGrading>();
        cA = mainCamPP.profile.GetSetting<ChromaticAberration>();
    }
	
	// Update
	void Update ()
    {
        autoExpo = mainCamPP.profile.GetSetting<AutoExposure>();

        if (position == 1 && myPlayer.isDream == false && isLaunched == false)
        {
            StartCoroutine("PP_Transition_To_Nightmare");
            isLaunched = true;
        }
        else if (position == 2 && myPlayer.isDream == true && isLaunched == false)
        {
            StartCoroutine("PP_Transition_To_Dream");
            isLaunched = true;
        }

        if(autoExpo.minLuminance.value == 0 && autoExpo.maxLuminance.value == 0)
        {
            isLaunched = false;
        }
    }

    IEnumerator PP_Transition_To_Nightmare()
    {
        position++;
        for (float i = autoExpo.minLuminance.value; i >= autoExpoLowAim; i = autoExpo.minLuminance.value - autoExpoStep)
        {
            autoExpo.minLuminance.value = autoExpo.minLuminance.value - autoExpoStep;
            autoExpo.maxLuminance.value = autoExpo.minLuminance.value;
            yield return new WaitForSeconds(rate);

            if (i == autoExpoLowAim)
            {
                for (float j = autoExpo.maxLuminance.value; j <= autoExpoHighAim; j = autoExpo.maxLuminance.value + autoExpoStep)
                {
                    autoExpo.maxLuminance.value = autoExpo.maxLuminance.value + autoExpoStep;
                    autoExpo.minLuminance.value = autoExpo.maxLuminance.value;
                    yield return new WaitForSeconds(rate);
                }
                colorGrading.mixerRedOutRedIn.value = 140f;
                colorGrading.mixerGreenOutGreenIn.value = 100f;
                colorGrading.contrast.value = 20f;
                colorGrading.lift.overrideState = true;
                vignette.intensity.value = 0.20f;
                grain.intensity.value = 0.35f;
                cA.intensity.value = 0.09f;
                break;
            }
        }
    }

    IEnumerator PP_Transition_To_Dream()
    {
        position--;
        for (float i = autoExpo.minLuminance.value; i >= autoExpoLowAim; i = autoExpo.minLuminance.value - autoExpoStep)
        {
            autoExpo.minLuminance.value = autoExpo.minLuminance.value - autoExpoStep;
            autoExpo.maxLuminance.value = autoExpo.minLuminance.value;
            yield return new WaitForSeconds(rate);

            if ( i == autoExpoLowAim)
            {
                for (float j = autoExpo.maxLuminance.value; j <= autoExpoHighAim; j = autoExpo.maxLuminance.value + autoExpoStep)
                {
                    autoExpo.maxLuminance.value = autoExpo.maxLuminance.value + autoExpoStep;
                    autoExpo.minLuminance.value = autoExpo.maxLuminance.value;
                    yield return new WaitForSeconds(rate);
                }
                colorGrading.mixerRedOutRedIn.value = 115f;
                colorGrading.mixerGreenOutGreenIn.value = 110f;
                colorGrading.contrast.value = 10f;
                colorGrading.lift.overrideState = false;
                vignette.intensity.value = 0f;
                grain.intensity.value = 0f;
                cA.intensity.value = 0f;
                break;
            }
        }
    }
}

/*for (float j = autoExpo.minLuminance.value; j >= autoExpoHighAim; j = autoExpo.minLuminance.value - autoExpoStep)
{
    autoExpo.minLuminance.value = autoExpo.minLuminance.value - autoExpoStep;
    autoExpo.maxLuminance.value = autoExpo.minLuminance.value;
    yield return new WaitForSeconds(rate);
}*/
