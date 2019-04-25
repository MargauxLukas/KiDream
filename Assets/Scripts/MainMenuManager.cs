using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class MainMenuManager : MonoBehaviour
{

    public PostProcessVolume myPPV;

    //Bloom
    Bloom myBloomSettings = null;
    [SerializeField]
    private int bloomIntensity = 10;

    public ParticleSystem backgroundSystem;


    public List<Button> buttonList = new List<Button>();
    public List<TextMeshProUGUI> buttonTextList = new List<TextMeshProUGUI>();

    public GameObject camera;
    Camera myCam;

    bool isInDreamStyle = false;
    bool canChangeStyle = true;
    float rightTrigger = 0f;
    float leftTrigger = 0f;


    void Start()
    {
        myPPV.profile.TryGetSettings(out myBloomSettings);
    }

    void Update()
    {
        leftTrigger = Input.GetAxis("Fire1");
        Debug.Log(leftTrigger);
        rightTrigger = Input.GetAxis("Fire2");

        //Triggers to change the style
        if((leftTrigger != 0 || rightTrigger != 0 || Input.GetKeyDown(KeyCode.Space)) && canChangeStyle == true)
        {
            ChangeWorldStyle();
        }

        //Anti-flood des triggers
        if(leftTrigger > 0.8f || rightTrigger > 0.8f)
        {
            canChangeStyle = false;
        }
        else
        {
            canChangeStyle = true;
        }

    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene("LON_Scene1", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Exit");
    }

    public void GetInOptionsMenu()
    {
        SceneManager.LoadScene("LON_MENU_OPTIONS", LoadSceneMode.Single);
    }
    
    public void ChangeWorldStyle()
    {

        //Passage en style Reve
        if (isInDreamStyle == false)
        {
            foreach (Button button in buttonList)
            {
                ColorBlock cb = button.colors;
                cb.normalColor = new Color32(255, 255, 255, 255);
                cb.highlightedColor = new Color32(255, 221, 177, 255);
                button.colors = cb;

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                int i = 0;
                buttonText.color = new Color32(0, 55, 255, 255);
            }

            myBloomSettings.intensity.value = 10f;

            myCam = camera.GetComponent<Camera>();
            myCam.backgroundColor = new Color32(0, 55, 255, 255);

            var main = backgroundSystem.main;
            main.startColor = new Color(255, 25, 255, 112);



            isInDreamStyle = true;
        }

        //Passage en style Cauchemar
        else if (isInDreamStyle == true)
        {
            foreach (Button button in buttonList)
            {
                ColorBlock cb = button.colors;
                cb.normalColor = new Color32(230, 0, 0, 210);
                cb.highlightedColor = new Color32(248, 97, 112, 218);
                button.colors = cb;

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.color = new Color32(27, 19, 41, 255);
            }

            myBloomSettings.intensity.value = 25f;

            myCam = camera.GetComponent<Camera>();
            myCam.backgroundColor = new Color32(27, 19, 41, 255);

            var main = backgroundSystem.main;
            main.startColor = new Color(184, 23, 23, 255);



            isInDreamStyle = false;
        }

    }


}
