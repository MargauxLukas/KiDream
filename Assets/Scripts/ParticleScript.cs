using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public bool isReve;
    private ParticleSystem cauchemarParticle;

	void Start ()
    {
        cauchemarParticle = GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = cauchemarParticle.emission;
	}
	
	// Update is called once per frame
	void Update ()
    {
        isReve = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().reve;
        ParticlePlay(isReve);
        
	}

    void ParticlePlay(bool isReve)
    {
        ParticleSystem.EmissionModule em = cauchemarParticle.emission;

        if (isReve)
        {
            cauchemarParticle.Clear();
            em.enabled = false;
        }
        else       { em.enabled = true ; }
    }


}
