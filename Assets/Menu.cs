using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public MenuReaction menuReaction;

	void OnParticleCollision (GameObject other)
    {
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