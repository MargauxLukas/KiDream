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
        FreezeAllExceptLast();
    }

    void FreezeAllExceptLast()
    {
        for(int i = 0; i < puzzleTab.Count-1; i++)
        {
            if (i < puzzleTab.Count)
            {
                puzzleTab[i].gameObject.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
            if(i == puzzleTab.Count-1)
            {
                puzzleTab[i].gameObject.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        }
    }
}
