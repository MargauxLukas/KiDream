using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckXaxis : MonoBehaviour
{
    public float minX;
    public float maxX;

    void Update()
    {
        if (transform.localPosition.x < minX)
        {
            transform.localPosition = new Vector2(minX, transform.localPosition.y);
        }

        if (transform.localPosition.x > maxX)
        {
            transform.localPosition = new Vector2(maxX, transform.localPosition.y);
        }
    }
}
