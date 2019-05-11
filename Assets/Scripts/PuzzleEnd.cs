using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEnd : MonoBehaviour
{
    public bool isConnected = false;
    public GameObject puzzle;

    private void Update()
    {
        if(isConnected)
        {
            Debug.Log("Je suis connecté");
        }
    }
}
