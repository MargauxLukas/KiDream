using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SoundSliderParticles : MonoBehaviour
{
    float lastValue;
    float newValue;
    public Slider mySlider;
    public ParticleSystem myPS;


	// Start
	void Start ()
    {
        lastValue = mySlider.value = 50f;
	}
	
	// Update
	void Update ()
    {

        newValue = mySlider.value;
        mySlider.onValueChanged.AddListener(delegate {ValueChange();} );

	}


    private void ValueChange()
    {

        /*
        if(newValue > lastValue)
        {
            var main = myPS.main;
            main.startColor = new Color(255, 0, 0, 255);
            main.startSize = 0.09f;
            main.startLifetime = 0.1f;
            main.startSpeed = 0.5f;

            var emission = myPS.emission;
            emission.rateOverDistance = 10f;

            var shape = myPS.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;

            this.transform.localEulerAngles = new Vector3(0, -90, 90);
            lastValue = newValue;
        }
        else if(newValue < lastValue)
        {
            var main = myPS.main;
            main.startColor = new Color(255, 0, 0, 100);
            main.startSize = 0.05f;
            main.startLifetime = 0.1f;
            main.startSpeed = 0.5f;

            var emission = myPS.emission;
            emission.rateOverDistance = 20f;

            var shape = myPS.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;

            this.transform.localEulerAngles = new Vector3(180, -90, 90);
            lastValue = newValue;
        }*/



    }



}
