using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomParticles : MonoBehaviour
{

    public List<Material> materialList = new List<Material>();

    ParticleSystem myParticleSystem;
    Renderer myPsRenderer;

    private bool canRandomize = true;

    // Start
    void Start ()
    {
        myParticleSystem = this.GetComponent<ParticleSystem>();
        myPsRenderer = myParticleSystem.GetComponent<Renderer>();
	}
	
	// Update
	void Update ()
    {
        RandomizeParticles();
	}

    public void RandomizeParticles()
    {
        if (canRandomize == true)
        {
            myPsRenderer.material = materialList[Random.Range(0, materialList.Count)];
            myParticleSystem.Emit(1);
        }
    }

}
