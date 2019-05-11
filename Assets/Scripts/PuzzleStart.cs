using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStart : MonoBehaviour
{
    public bool isConnected = true;
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
    }
}
