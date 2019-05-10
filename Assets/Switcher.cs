using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{

    private bool dream = true;

    public GameObject dreamObject;
    public GameObject nightmareObject;

    private bool selectorInUse;

	// Start
	void Start ()
    {
		
	}
	
	// Update
	void Update ()
    {
        float selector = Input.GetAxisRaw("ChangeWorld");

        if (selector > 0 && dream == false && selectorInUse == false)
        {
            dreamObject.SetActive(true);
            nightmareObject.SetActive(false);

            dream = true;
            selectorInUse = true;

        }
        else if (selector > 0 && dream == true && selectorInUse == false)
        {
            dreamObject.SetActive(false);
            nightmareObject.SetActive(true);

            dream = false;
            selectorInUse = true;
        }
        
        if(selector == 0)
        {
            selectorInUse = false;
        }
    }
}
