using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInter : MonoBehaviour
{
    public bool isConnected = false;
    public GameObject puzzle;
    private int i;

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.GetComponent<PuzzleInter>().isConnected == false)
            {
                i = puzzle.GetComponent<PuzzleManager>().nbSave;
                collision.GetComponent<PuzzleInter>().isConnected = true;
                puzzle.GetComponent<PuzzleManager>().puzzleTab[i] = collision.gameObject;
            }

            else if (collision.GetComponent<PuzzleEnd>().isConnected == false)
            {
                i = puzzle.GetComponent<PuzzleManager>().nbSave;
                collision.GetComponent<PuzzleEnd>().isConnected = true;
                puzzle.GetComponent<PuzzleManager>().puzzleTab[i] = collision.gameObject;
            }
            else
            {
                //Inactif
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isConnected = false;
        if(collision.GetComponent<PuzzleInter>())
        {
            collision.GetComponent<PuzzleInter>().isConnected = false;
        }
        if (collision.GetComponent<PuzzleEnd>())
        {
            collision.GetComponent<PuzzleEnd>().isConnected = false;
        }
    }
}
