using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckXaxis : MonoBehaviour
{
    public float minX;
    public float maxX;

    void Update()
    {
        if (transform.position.x < minX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }

        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }
    }
}
