using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsColor : MonoBehaviour
{

    public Image rb;
    public Image lb;
    public Image rightTrigger;

    CharacterController myPlayer;

	// Start
	void Start ()
    {
        myPlayer = FindObjectOfType<CharacterController>();
        this.gameObject.SetActive(false); // initialisation
	}
	
	// Update
	void Update ()
    {
		if(myPlayer.isDream == true)
        {
            rb.color = new Color32(0, 255, 255, 255);
            lb.color = new Color32(0, 255, 255, 255);
            rightTrigger.color = new Color32(0, 255, 255, 255);
        }
        else if(myPlayer.isDream == false)
        {
            rb.color = new Color32(255, 255, 255, 255);
            lb.color = new Color32(255, 255, 255, 255);
            rightTrigger.color = new Color32(255, 255, 255, 255);
        }

        if(PauseMenu.optionsIndex == 0)
        {
            rb.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(88.7f, 2f, 0f);
            lb.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(15.3f, 2f, 0f);
        }
        else if(PauseMenu.optionsIndex == 1)
        {
            rb.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(64.3f, -24.2f, 0f);
            lb.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(22.3f, -24.2f, 0f);
        }
    }
}
