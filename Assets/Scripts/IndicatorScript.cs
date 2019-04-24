using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{

    SpriteRenderer myRenderer;

	// Start
	void Start ()
    {
        myRenderer = this.GetComponent<SpriteRenderer>();

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DisplayIndicator();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HideIndicator();
        }
    }


    public void DisplayIndicator()
    {
        myRenderer.enabled = true;
    }

    public void HideIndicator()
    {
        myRenderer.enabled = false;
    }

}
