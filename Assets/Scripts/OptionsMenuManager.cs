using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{

	// Start
	void Start ()
    {
		
	}
	
	// Update
	void Update ()
    {
		
	}


    public void BackToMainMenu()
    {
        SceneManager.LoadScene("LON_MENU", LoadSceneMode.Single);
    }



}
