using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionToWave : MonoBehaviour
{
    public ParticleSystem shooter;

    [SerializeField]
    private float VerticalForceToPush = 0f;

    [SerializeField]
    private float HorizontalForceToPush = 0f;

    public WaveManager waveManager;

	// Use this for initialization
	void Start()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        shooter = other.GetComponent<ParticleSystem>();

        switch (waveManager.waveSelection)
        {
            case WaveType.Push:
                rb.AddForce(new Vector2(-(shooter.transform.position.x - this.transform.position.x), -(shooter.transform.position.y - this.transform.position.y)));

                break;

            case WaveType.Pull:
                rb.AddForce(new Vector2((shooter.transform.position.x - this.transform.position.x), (shooter.transform.position.y - this.transform.position.y)));

                break;

            case WaveType.Activate:

                break;

            case WaveType.PushCorruption:

                break;

            case WaveType.PullCorruption:

                break;

            case WaveType.ActivateCorruption:

                break;
        }

    }

}
