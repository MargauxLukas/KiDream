using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInter : MonoBehaviour
{
    public bool isConnected = false;
    private bool isAlreadyHere = false;
    public GameObject puzzle;
    private int i;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isConnected) { return; }
        if (collision.GetComponent<PuzzleStart>())
        {
            isConnected = true;
            puzzle.GetComponent<PuzzleManager>().puzzleTab.Add(gameObject);
        }
        else if (collision.GetComponent<PuzzleInter>().isConnected == true)
        {
            isConnected = true;
            puzzle.GetComponent<PuzzleManager>().puzzleTab.Add(gameObject);
        }
        else if (collision.GetComponent<PuzzleEnd>())
        {
            collision.GetComponent<PuzzleEnd>().isConnected = true;
            puzzle.GetComponent<PuzzleManager>().puzzleTab.Add(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isConnected = false;
        puzzle.GetComponent<PuzzleManager>().puzzleTab.Remove(gameObject);

    }
}
