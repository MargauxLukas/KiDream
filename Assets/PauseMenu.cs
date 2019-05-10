using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuDream;
    public GameObject pauseMenuNightmare;

    public CharacterController myPlayer;

    public ButtonBehaviour buttonBehaviour;

    public GameObject indicator;

    public GameObject optionsGO;
    public GameObject controlsGO;

    public List<GameObject> InputsGoList = new List<GameObject>();


    // Start
    void Start()
    {

    }

    // Update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) && this.gameObject.name == "PauseMenu")
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        indicator.SetActive(true);

        switch(buttonBehaviour)
        {
            case ButtonBehaviour.Resume:
                Resume();
                break;
            case ButtonBehaviour.Options:
                controlsGO.SetActive(false);
                optionsGO.SetActive(true);
                break;
            case ButtonBehaviour.Controls:
                optionsGO.SetActive(false);
                controlsGO.SetActive(true);
                break;
            case ButtonBehaviour.Quit:
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        indicator.SetActive(false);
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
public enum ButtonBehaviour { Resume, Options, Controls, Quit}