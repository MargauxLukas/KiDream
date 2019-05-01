using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public List<Image> imageList = new List<Image>();
    public CharacterController myPlayer;
    public WaveManager waveManager;
    public RectTransform selectionIndicator;

    public Slider manaBar;

	// Start
	void Start ()
    {
		
	}
	
	// Update
	void Update ()
    {
		if(myPlayer.isDream == true)
        {
            foreach(Image im in imageList)
            {
                if(im.gameObject.name.Contains("Dream"))
                {
                    im.gameObject.SetActive(true);
                }
                else
                {
                    im.gameObject.SetActive(false);
                }
            }
        }
        else if(myPlayer.isDream == false)
        {
            foreach (Image im in imageList)
            {
                if (im.gameObject.name.Contains("Nightmare"))
                {
                    im.gameObject.SetActive(true);
                }
                else
                {
                    im.gameObject.SetActive(false);
                }
            }
        }

        if(selectionIndicator.position != imageList[waveManager.selectionIndex].rectTransform.position)
        {
            selectionIndicator.position = imageList[waveManager.selectionIndex].rectTransform.position;
        }

        

	}
}
