using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] puzzleTab = new GameObject[9];
    public  int     nbSave;
    private bool noConnect = false;

    private void Update()
    {
        for(int i = 0; i< puzzleTab.Length;i++)
        {
            if(puzzleTab[i]==null)
            {
                nbSave = i;
                break;
            }
        }

        checkFunction();

    }

    void checkFunction()
    {
        for (int i = 0; i < puzzleTab.Length; i++)
        {
            if (puzzleTab[i] != null)
            {
                Debug.Log(puzzleTab[i]);
                if (noConnect)
                {
                    puzzleTab[i].GetComponent<PuzzleInter>().isConnected = false;
                    puzzleTab[i] = null;
                }

                if (puzzleTab[i].GetComponent<PuzzleInter>().isConnected == false)
                {
                    Debug.Log("Je rentre là");
                    puzzleTab[i] = null;
                    noConnect = true;
                }
            }
        }
    }
}
