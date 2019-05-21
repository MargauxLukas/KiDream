using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckParticular : MonoBehaviour
{
    public float yPallier;
    public float xPallier;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.localPosition.y > yPallier)
        {
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; 
        }
        else if (transform.localPosition.x > xPallier)
        {
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
