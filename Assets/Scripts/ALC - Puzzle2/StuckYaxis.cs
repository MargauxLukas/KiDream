using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckYaxis : MonoBehaviour
{
    public float minY;
    public float maxY;

    void Update()
    {
        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }

        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
        }
    }
}
