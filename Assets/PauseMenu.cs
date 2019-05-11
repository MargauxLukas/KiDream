using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public static bool optionsSelected = false;

    public GameObject pauseMenuDream;
    public GameObject pauseMenuNightmare;

    public CharacterController myPlayer;

    public ButtonBehaviour buttonBehaviour;

    public GameObject indicator;

    public GameObject optionsGO;
    public GameObject controlsGO;

    public List<GameObject> InputsGoList = new List<GameObject>();

    private bool shootAxisInUse;
    float shootAxis;

    public PauseMenu pauseMenuManager;

    public RectTransform handlePosition;
    public RectTransform handleMin;
    public RectTransform handleMax;
    public float handleStep;

    public static float handleReturnedValue;

    public GameObject aiguille;
    private int optionsIndex = 0;

    private void Awake()
    {
        if (this.gameObject.name.Contains("Options") && myPlayer.isDream == true)
        {
            handleReturnedValue = (handlePosition.localPosition.x - handleMin.localPosition.x) / (handleMax.localPosition.x - handleMin.localPosition.x);
        }
        else if (this.gameObject.name.Contains("Options") && myPlayer.isDream == false)
        {
            handlePosition.localPosition = new Vector2((handleReturnedValue*(handleMax.localPosition.x-handleMin.localPosition.x)) + handleMin.localPosition.x, handlePosition.localPosition.y);
        }
    }

    // Start
    void Start()
    {

    }

    // Update
    void Update()
    {
        shootAxis = Input.GetAxisRaw("ShootParticles");
        
        if (shootAxis == 0)
        {
            shootAxisInUse = false;
        }

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

        if(this.gameObject.name.Contains("Options"))
        {
            InOptionsMenu();
        }

    }

    //TESTER LE CHANGEMENT DE SON AVEC L'AUDIO MIXER

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.name != "PauseMenu")
        {
            pauseMenuManager.indicator = indicator;
        }

        if (collision.CompareTag("Aiguille"))
        {
            indicator.SetActive(true);

            if(shootAxis > 0 && shootAxisInUse == false && optionsSelected == false)
            {
                switch (buttonBehaviour)
                {
                    case ButtonBehaviour.Resume:
                        Resume();
                        controlsGO.SetActive(false);
                        optionsGO.SetActive(false);
                        break;

                    case ButtonBehaviour.Options:
                        controlsGO.SetActive(false); // Controls FALSE
                        optionsGO.SetActive(true); // Options TRUE
                        optionsSelected = true;
                        break;

                    case ButtonBehaviour.Controls:
                        controlsGO.SetActive(true); // Controls TRUE
                        optionsGO.SetActive(false); // Options FALSE
                        break;

                    case ButtonBehaviour.Quit:
                        controlsGO.SetActive(false);
                        optionsGO.SetActive(false);
                        SceneManager.LoadScene(0);
                        break;
                }

                shootAxisInUse = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Aiguille"))
        {
            indicator.SetActive(false);
        }
    }

    public void Resume()
    {
        indicator.SetActive(false);
        optionsSelected = false;

        pauseMenuDream.SetActive(false);
        pauseMenuNightmare.SetActive(false);

        foreach (GameObject go in pauseMenuManager.InputsGoList)
        {
            go.SetActive(true);
        }

        //Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        if(this.gameObject.name.Contains("Options"))
        {
            handlePosition.localPosition = new Vector2((handleReturnedValue * (handleMax.localPosition.x - handleMin.localPosition.x)) + handleMin.localPosition.x, handlePosition.localPosition.y);
        }

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

        //Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void InOptionsMenu()
    {

        handlePosition.localPosition = new Vector2(Mathf.Clamp(handlePosition.localPosition.x, handleMin.localPosition.x + 0.1f, handleMax.localPosition.x - 0.1f), handlePosition.localPosition.y);        

        if (optionsSelected == true)
        {
            aiguille.SetActive(false);

            if (shootAxis > 0 && shootAxisInUse == false)
            {
                optionsIndex++;
            }
                switch (optionsIndex)
                {
                    case 0:
                        if (handlePosition.localPosition.x > handleMin.localPosition.x && handlePosition.localPosition.x < handleMax.localPosition.x)
                        {
                            if (Input.GetKey(KeyCode.Joystick1Button4))
                            {
                                handlePosition.localPosition = new Vector2(handlePosition.localPosition.x - handleStep, handlePosition.localPosition.y);
                            }
                            else if (Input.GetKey(KeyCode.Joystick1Button5))
                            {
                                handlePosition.localPosition = new Vector2(handlePosition.localPosition.x + handleStep, handlePosition.localPosition.y);
                            }

                        handleReturnedValue = (handlePosition.localPosition.x - handleMin.localPosition.x) / (handleMax.localPosition.x - handleMin.localPosition.x);

                        }
                    break;
                    case 1:

                        break;
                    case 2:

                        break;

                }
            

        }
        else
        {
            aiguille.SetActive(true);
        }

    }

}
public enum ButtonBehaviour { Resume, Options, Controls, Quit}