using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenePuzzle1ToBossIntro : MonoBehaviour
{
    private string level = "ALC_BossIntro";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(level);
        }
    }
}

