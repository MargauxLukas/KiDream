using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextLangManager : MonoBehaviour {

    public GameObject luminosite;
    public GameObject volume;
    public GameObject musique;
    public GameObject langue;

    Text textLight;
    Text textVolume;
    Text textMusic;
    Text textLang;

    SpeechData data;

    private void Awake()
    {
        textLight = luminosite.GetComponentInChildren<Text>();
        textMusic = musique.GetComponentInChildren<Text>();
        textLang = langue.GetComponentInChildren<Text>();

        data = Resources.Load<SpeechData>("Dialogues/OptionsMenu");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(GameLanguage.lang == Language.french)
        {
            textLight.text = data.speechFR[0];
            textMusic.text = data.speechFR[2];
            textLang.text = data.speechFR[3];
        }
        else
        {
            textLight.text = data.speechGB[0];
            textMusic.text = data.speechGB[2];
            textLang.text = data.speechGB[3];
        }
	}
}
