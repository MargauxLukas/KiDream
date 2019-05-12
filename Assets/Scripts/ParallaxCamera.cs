using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    public CharacterController myPlayer;

    private float oldPositionX;
    private float oldPositionY;

	void Start ()
    {
        oldPositionX = transform.position.x;
        oldPositionY = transform.position.y;
	}

	void Update ()
    {

        if (myPlayer.isDream)
        {
            GetComponent<Camera>().backgroundColor = new Color32(218, 147, 137, 255);
        }
        else if (!myPlayer.isDream)
        {
            GetComponent<Camera>().backgroundColor = new Color32(96, 31, 62, 255);
        }

        if (transform.position.x != oldPositionX)
        {
            if (onCameraTranslate != null)
            {
                float delta = oldPositionX - transform.position.x;
                onCameraTranslate(delta);
            }

            oldPositionX = transform.position.x;
        }

        if (transform.position.y != oldPositionY)
        {
            if (onCameraTranslate != null)
            {
                float delta = oldPositionY - transform.position.y;
                onCameraTranslate(delta);
            }

            oldPositionY = transform.position.y;
        }
    }
}
