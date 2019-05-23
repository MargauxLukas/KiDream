using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public List<Image> imageList = new List<Image>();
    public CharacterController myPlayer;
    public WaveManager waveManager;

    public Image lifeBar;
    public Image manaBar;

    public GameObject dreamUIBars;
    public GameObject nightmareUIBars;

    public bool locked = false;

	// Start
	void Start ()
    {

    }
	
	// Update
	void Update ()
    {
        PlayerStatut();

        switch (waveManager.selectionIndex)
        {
            case 0:
                ImageSelection();
                imageList[0].gameObject.SetActive(true);
                break;
            case 1:
                ImageSelection();
                imageList[1].gameObject.SetActive(true);
                break;
            case 2:
                ImageSelection();
                imageList[2].gameObject.SetActive(true);
                break;
            case 3:
                ImageSelection();
                imageList[3].gameObject.SetActive(true);
                break;
            case 4:
                ImageSelection();
                imageList[4].gameObject.SetActive(true);
                break;
            case 5:
                ImageSelection();
                imageList[5].gameObject.SetActive(true);
                break;
        }
	}

    public void ImageSelection()
    {
        foreach (Image im in imageList)
        {
                im.gameObject.SetActive(false);
        }
    }

    public void PlayerStatut()
    {
        switch(myPlayer.isDream)
        {
            case true:
                dreamUIBars.SetActive(true);
                nightmareUIBars.SetActive(false);
                lifeBar = dreamUIBars.transform.Find("HP_Fill_Dream").GetComponent<Image>();
                manaBar = dreamUIBars.transform.Find("Mana_Fill_Dream").GetComponent<Image>();

                if(locked == false)
                {
                    manaBar.fillAmount = WaveManager.manaBarValue;
                    locked = true;
                }

                UILifeSetter();
                break;

            case false:
                nightmareUIBars.SetActive(true);
                dreamUIBars.SetActive(false);
                lifeBar = nightmareUIBars.transform.Find("HP_Fill_Nightmare").GetComponent<Image>();
                manaBar = nightmareUIBars.transform.Find("Mana_Fill_Nightmare").GetComponent<Image>();

                if(locked == true)
                {
                    manaBar.fillAmount = WaveManager.manaBarValue;
                    locked = false;
                }

                UILifeSetter();
                break;
        }
    }

    public void UILifeSetter()
    {
        switch(myPlayer.hp)
        {
            case 3:
                lifeBar.fillAmount = 1f;
                break;
            case 2:
                lifeBar.fillAmount = 0.575f;
                break;
            case 1:
                lifeBar.fillAmount = 0.185f;
                break;
            case 0:
                lifeBar.fillAmount = 0f;
                break;
        }
    }
}
