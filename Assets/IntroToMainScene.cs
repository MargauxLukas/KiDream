using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroToMainScene : MonoBehaviour
{
    private string level = "MAL_Puzzle01";

    public void loadlevel()
    {
        SceneManager.LoadScene(level);
    }
}
