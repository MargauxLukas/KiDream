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
                vignette.intensity.value = 0.189f;
                grain.intensity.value = 0.4f;
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
                vignette.intensity.value = 0f;
                grain.intensity.value = 0f;
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
