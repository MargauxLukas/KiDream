using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixOrderInLayer : MonoBehaviour
{
    SpriteRenderer sprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SpriteRenderer>() != null)
        {
            sprite = collision.GetComponent<SpriteRenderer>();
            sprite.sortingOrder = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<SpriteRenderer>() != null)
        {
            sprite = collision.GetComponent<SpriteRenderer>();
            sprite.sortingOrder = 0;
        }
    }
}
