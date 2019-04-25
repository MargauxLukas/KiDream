using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEnter : MonoBehaviour
{
    public SpriteRenderer escalier;
    public BoxCollider2D wallDisapear;


    void Start()
    {
        escalier.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "CubeD")
        {
            wallDisapear.isTrigger = true;
            escalier.enabled = true;
        }
    }
}
