using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public List<Image> imageList = new List<Image>();
    public CharacterController myPlayer;
    public WaveManager waveManager;

    public Slider manaBar;

	// Start
	void Start ()
    {
		
	}
	
	// Update
	void Update ()
    {
        switch(waveManager.selectionIndex)
        {
            case 0:
                ImageSeleciton();
                imageList[0].gameObject.SetActive(true);
                break;
            case 1:
                ImageSeleciton();
                imageList[1].gameObject.SetActive(true);
                break;
            case 2:
                ImageSeleciton();
                imageList[2].gameObject.SetActive(true);
                break;
            case 3:
                ImageSeleciton();
                imageList[3].gameObject.SetActive(true);
                break;
            case 4:
                ImageSeleciton();
                imageList[4].gameObject.SetActive(true);
                break;
            case 5:
                ImageSeleciton();
                imageList[5].gameObject.SetActive(true);
                break;
        }
	}

    public void ImageSeleciton()
    {
        foreach (Image im in imageList)
        {
                im.gameObject.SetActive(false);
        }
    }
}
