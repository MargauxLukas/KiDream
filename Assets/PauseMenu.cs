using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuDream;
    public GameObject pauseMenuNightmare;

    public CharacterController myPlayer;

    public List<GameObject> InputsGoList = new List<GameObject>();


    // Start
    void Start()
    {

    }

    // Update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (gameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        pauseMenuDream.SetActive(false);
        pauseMenuNightmare.SetActive(false);

        foreach (GameObject go in InputsGoList)
        {
            go.SetActive(true);
        }

        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        if (myPlayer.isDream == false)
        {
            pauseMenuNightmare.SetActive(true);
            pauseMenuDream.SetActive(false);
        }
        else if (myPlayer.isDream == true)
        {
            pauseMenuDream.SetActive(true);
            pauseMenuNightmare.SetActive(false);
        }

        foreach(GameObject go in InputsGoList)
        {
            go.SetActive(false);
        }

        Time.timeScale = 0f;
        gameIsPaused = true;
    }

}
