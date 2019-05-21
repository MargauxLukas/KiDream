using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{

    public PostProcessVolume mainCamPP;
    public CharacterController myPlayer;
    public PostProcessVolume secondPP;

    AutoExposure autoExpo;

    PostProcessVolume temp;

    public float autoExpoStep;
    public float autoExpoHighAim;
    public float autoExpoLowAim;

    public float rate;

    public int position = 1;

    public bool isLaunched;

	// Start
	void Start ()
    {

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
                temp = secondPP;
                //Debug.Log(temp.profile.name);

                secondPP.profile = mainCamPP.profile;
                //Debug.Log(mainCamPP.profile.name);
                //Debug.Log(temp.profile.name);

                mainCamPP.profile = temp.profile;
                //Debug.Log(secondPP.profile.name);

                for (float j = autoExpo.maxLuminance.value; j <= autoExpoHighAim; j = autoExpo.maxLuminance.value + autoExpoStep)
                {
                    autoExpo.maxLuminance.value = autoExpo.maxLuminance.value + autoExpoStep;
                    autoExpo.minLuminance.value = autoExpo.maxLuminance.value;
                    yield return new WaitForSeconds(rate);
                }
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
                temp = mainCamPP;
                //Debug.Log(temp.profile.name);

                mainCamPP.profile = secondPP.profile;
                //Debug.Log(mainCamPP.profile.name);

                secondPP.profile = temp.profile;
                //Debug.Log(secondPP.profile.name);


                for (float j = autoExpo.maxLuminance.value; j <= autoExpoHighAim; j = autoExpo.maxLuminance.value + autoExpoStep)
                {
                    autoExpo.maxLuminance.value = autoExpo.maxLuminance.value + autoExpoStep;
                    autoExpo.minLuminance.value = autoExpo.maxLuminance.value;
                    yield return new WaitForSeconds(rate);
                }
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
