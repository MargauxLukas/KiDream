using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckYaxis : MonoBehaviour
{
    public float minY;
    public float maxY;

    void Update()
    {
        if (transform.localPosition.y < minY)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, minY);
        }

        if (transform.localPosition.y > maxY)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, maxY);
        }
    }
}
