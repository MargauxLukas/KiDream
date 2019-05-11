using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<GameObject> puzzleTab;
    private bool noConnect = false;
    private int number = 0;

    void Update()
    {
        checkFunction();
    }

    void checkFunction()
    {

    }

    public int getNumber()
    {
        number++;
        return number;
        
    }
}
