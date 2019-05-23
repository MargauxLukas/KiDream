using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBestScript : MonoBehaviour
{
    public BoxCollider2D thisbc;
    public BoxCollider2D bccondition;


    // Update
    void Update ()
    {
        if(bccondition.enabled == false)
        {
            thisbc.enabled = false;
        }

	}
}
