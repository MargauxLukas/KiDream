using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPValue : MonoBehaviour
{

    public CharacterController myPlayer;
    SpriteRenderer displayedHPColor;
    List<Color> colorTraces = new List<Color>();

    public Color col;
    public float colorLoad;


    private void Awake()
    {
        displayedHPColor = GetComponent<SpriteRenderer>();
    }

    // Start
    void Start ()
    {

       
 
	}
	
	// Update
	void Update ()
    {
        if (myPlayer.hp < 0)
            myPlayer.hp = 0;
        
            colorLoad = (255 - (myPlayer.hp) * 255)*2;
            col = new Color(colorLoad / 255f, 1f, 0, 1f);
            displayedHPColor.color = col;

        if(myPlayer.hp < 0.5f)
        {
            colorLoad = (255 - (myPlayer.hp) / 255);
            col = new Color(1f, colorLoad / 255f, 0, 1f);
            displayedHPColor.color = col;
        }
    }
}
