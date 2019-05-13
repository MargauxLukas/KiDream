using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEnd : MonoBehaviour
{
    public GameObject puzzle;
    public bool isConnected = false;


    private void Update()
    {
        if(isConnected)
        {
            //TriggerStay ICI
            Debug.Log("Je suis connecté");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isConnected) { return; }
        if (collision.GetComponent<PuzzleInter>())
        {
            Debug.Log(collision.name);
            GetComponent<PuzzleEnd>().isConnected = true;
            puzzle.GetComponent<PuzzleManager>().puzzleTab.Add(gameObject);
        }
    }
}
