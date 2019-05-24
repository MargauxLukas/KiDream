using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsColor : MonoBehaviour
{

    public Image rb;
    public Image lb;

    CharacterController myPlayer;

	// Start
	void Start ()
    {
        myPlayer = FindObjectOfType<CharacterController>();
	}
	
	// Update
	void Update ()
    {

		if(myPlayer.isDream == true)
        {
            rb.color = new Color32(0, 255, 255, 255);
            lb.color = new Color32(0, 255, 255, 255);
        }
        else if(myPlayer.isDream == false)
        {
            rb.color = new Color32(255, 255, 255, 255);
            lb.color = new Color32(255, 255, 255, 255);
        }

	}
}
