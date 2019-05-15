using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckParticular : MonoBehaviour
{
    public float yPallier;
    public float xPallier;

    private void Update()
    {
        if(transform.localPosition.y > yPallier )
        {
            gameObject.GetComponent<StuckXaxis>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<StuckXaxis>().enabled = true;
        }

        if (transform.localPosition.x < xPallier)
        {
            gameObject.GetComponent<StuckYaxis>().enabled = true;
        }
        else
        {
            //gameObject.GetComponent<StuckYaxis>().enabled = false;
        }
    }
}
