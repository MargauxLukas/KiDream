using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeAmplifier : MonoBehaviour {

    [Range(0,20)]
    public float lifeTimeBonus;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem shooter = other.GetComponent<ParticleSystem>();

        ParticleSystem.Particle[] ParticleList = new ParticleSystem.Particle[shooter.particleCount];
        shooter.GetParticles(ParticleList);

        float dist = float.PositiveInfinity;

        int indexParticle = 0;

        for (int i = 0; i < ParticleList.Length; ++i)
        {
            if ((ParticleList[i].position - transform.position).magnitude < dist)
            {
                dist = (ParticleList[i].position - transform.position).magnitude;
                indexParticle = i;
            }
        }

        ParticleList[indexParticle].remainingLifetime = ParticleList[indexParticle].remainingLifetime + lifeTimeBonus;

        shooter.SetParticles(ParticleList, shooter.particleCount);
    }
}
