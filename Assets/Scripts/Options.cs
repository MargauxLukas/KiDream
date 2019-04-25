using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Options : MonoBehaviour {

    public GameObject lightObject;
    public GameObject lightSlider;
    public AudioSource audio;
    public Button muteButton;
    public Dropdown musicDropdown;
    ColorBlock colorblock;
    SpriteRenderer light;
    Slider slider;
    Color alphaColor;

    Text muteText;


    public AudioClip[] audioList;


    // Use this for initialization
    void Start () {
        light = lightObject.GetComponent<SpriteRenderer>();
        slider = lightSlider.GetComponent<Slider>();
        muteText = muteButton.GetComponentInChildren<Text>();

        audioList = new AudioClip[]
        {
            Resources.Load<AudioClip>("Music/ParachuteEnding"),
            Resources.Load<AudioClip>("Music/Satisfaction"),
            Resources.Load<AudioClip>("Music/WehDemAFEelLike")
        };
    }

    public void LightModifier()
    {
        alphaColor = light.GetComponent<SpriteRenderer>().color = new Color(0,0,0,slider.value/255);
    }

    public void Mute()
    {
        if(audio.volume !=0)
        {
            muteText.text = "OFF";
            muteText.color = Color.red;
            audio.volume = 0;
        }

        else if (audio.volume == 0)
        {
            muteText.text = "ON";
            muteText.color = Color.green;
            audio.volume = 0.75f;
        }

    }

    public void VolumeButtonEnable()
    {
        colorblock = muteButton.colors;
        colorblock.highlightedColor = new Color32(255, 255, 255, 255);
        muteButton.colors = colorblock;
    }

    public void VolumeButtonDisable()
    {
        colorblock = muteButton.colors;
        colorblock.highlightedColor = new Color32(0, 0, 0, 0);
        muteButton.colors = colorblock;
    }

    public void MusicChanger()
    {
        int i = musicDropdown.value;
        audio.clip = audioList[i];
        audio.Play();
    }

    public void SetFrench()
    {
        GameLanguage.lang = Language.french;
    }

    public void SetEnglish()
    {
        GameLanguage.lang = Language.english;
    }

}
 
 