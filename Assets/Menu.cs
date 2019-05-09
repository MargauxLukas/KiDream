using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private bool selectorInUse;

    public SpriteRenderer indicator;

    public GameObject progressionBar;
    //public GameObject finalPosition;

    public AudioSource as0;
    public AudioSource as1;

    public Animation loadingScreenPop;

    public MenuReaction menuReaction;

    private void Update()
    {

    }

    void OnTriggerStay2D (Collider2D collision)
    {
        float selector = Input.GetAxisRaw("ShootParticles");

        indicator.enabled = true;

        if(selector > 0 && selectorInUse == false)
        {
            selectorInUse = true;

		    switch(menuReaction)
            {
                case MenuReaction.MainMenu:
                    SceneLoader(0);
                    break;
                case MenuReaction.PlayMenu:
                    SceneLoader(1);
                    break;
                case MenuReaction.NewGame:
                    loadingScreenPop.Play();
                    StartCoroutine(LoadAsynchronously(6));
                    break;
                case MenuReaction.ChapterMenu:
                    SceneLoader(3);
                    break;
                case MenuReaction.OptionsMenu:
                    SceneLoader(4);
                    break;
                case MenuReaction.QuitPopUp:
                    SceneLoader(5);
                    break;
                case MenuReaction.QuitGame:
                    QuitGame();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(as0.isPlaying == false)
        {
            as0.Play();
        }
        else if(as0.isPlaying == true)
        {
            if (as1.isPlaying == true)
            {
                return;
            }
            else
            {
                as1.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        indicator.enabled = false;
    }

    public void SceneLoader(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Goodbye !");
    }

    IEnumerator LoadAsynchronously(int index)
    {
        //yield return new WaitForSeconds(loadingScreenPop.clip.length);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while(operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            progressionBar.transform.localPosition = new Vector3(((0 - progressionBar.transform.localPosition.x) * progress - 3.18636f), progressionBar.transform.localPosition.y, progressionBar.transform.localPosition.z);

            yield return null;
        }
    }
    //205 - 611
    // -700 - 0
    //-3.186363

}

public enum MenuReaction {MainMenu, PlayMenu, NewGame, ChapterMenu, OptionsMenu, QuitPopUp, QuitGame}