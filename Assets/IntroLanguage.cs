using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroLanguage : MonoBehaviour
{

    public List<Text> GBTextList = new List<Text>();
    public List<Text> FRTextList = new List<Text>();

    private bool locked = false;

	// Start
	void Start ()
    {
		
	}
	
	// Update
	void Update ()
    {
        if(GameLanguage.lang == Language.english && locked == false)
        {
            foreach(Text txt in GBTextList)
            {
                txt.gameObject.SetActive(true);
            }

            foreach (Text txt in FRTextList)
            {
                txt.gameObject.SetActive(false);
            }

            locked = true;
        }
        else if(GameLanguage.lang == Language.french && locked == true)
        {
            foreach (Text txt in GBTextList)
            {
                txt.gameObject.SetActive(false);
            }

            foreach (Text txt in FRTextList)
            {
                txt.gameObject.SetActive(true);
            }

            locked = false;
        }
    }
}
