using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{

    Text myText;

    public string englishText;
    public string frenchText;

    public bool aah;

	// Start
	void Start ()
    {
        myText= this.GetComponent<Text>();
	}
	
	// Update
	void Update ()
    {
		if(GameLanguage.lang == Language.english)
        {
            myText.text = englishText;
        }
        else if (GameLanguage.lang == Language.french)
        {
            myText.text = frenchText;
        }

        if(aah == true)
        {
            GameLanguage.lang = Language.french;
        }
        else
        {
            GameLanguage.lang = Language.english;
        }
	}
}
