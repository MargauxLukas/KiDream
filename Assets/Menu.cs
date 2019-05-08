using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private bool selectionInUse;

    public SpriteRenderer indicator;

    public AudioSource as0;
    public AudioSource as1;

    public MenuReaction menuReaction;

	void OnTriggerStay2D (Collider2D collision)
    {
        float selection = Input.GetAxisRaw("ShootParticles");

        indicator.enabled = true;

        if(selection > 0 && selectionInUse == false)
        {
            Debug.Log("1");
            selectionInUse = true;
            Debug.Log("2");

		    switch(menuReaction)
            {
                case MenuReaction.MainMenu:
                    SceneLoader(0);
                    break;
                case MenuReaction.PlayMenu:
                    SceneLoader(1);
                    break;
                case MenuReaction.NewGame:
                    SceneLoader(2);
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
}

public enum MenuReaction {MainMenu, PlayMenu, NewGame, ChapterMenu, OptionsMenu, QuitPopUp, QuitGame}