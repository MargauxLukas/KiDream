using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStart : MonoBehaviour
{
    public bool isConnected = true;
    public GameObject puzzle;

    private int i;

    private void Start()
    {
        puzzle.GetComponent<PuzzleManager>().puzzleTab.Add(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            
    }
}
