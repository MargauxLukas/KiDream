using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionToWave : MonoBehaviour
{

    public List<ParticleSystem> myPSList = new List<ParticleSystem>();

    private ParticleSystem shooter;

    public WaveManager waveManager;

    [Header("Propriétés")]
    public bool canBePushed = false;
    public bool canBePulled = false;
    public bool canBeActivated = false;
    public bool canBePushCorrupted = false;
    public bool canBePullCorrupted = false;
    public bool canBeActivateCorrupted = false;

    [Header("Push force")]
    [Range(0, 50), SerializeField]
    private float VerticalPush = 1f;
    [Range(0, 50), SerializeField]
    private float HorizontalPush = 1f;

    [Header("Pull force")]
    [Range(0, 50), SerializeField]
    private float VerticalPull = 1f;
    [Range(0, 50), SerializeField]
    private float HorizontalPull = 1f;

    [Header("Activate options")]
    public GameObject connectedGameObject;
    public ActivateBehaviour activateBehaviour;

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
                    if(canBePushed == true)
                    {
                        rb.AddForce(new Vector2(-(shooter.transform.position.x - this.transform.position.x) * HorizontalPush, -(shooter.transform.position.y - this.transform.position.y) * VerticalPush));
                    }
                    break;

                case WaveType.Pull:
                    if(canBePulled == true)
                    {
                        rb.AddForce(new Vector2((shooter.transform.position.x - this.transform.position.x) * HorizontalPull, (shooter.transform.position.y - this.transform.position.y) * VerticalPull));
                    }
                    break;

                case WaveType.Activate:
                    if(canBeActivated == true && connectedGameObject != null)
                    {
                        switch (activateBehaviour)
                        {

                        case ActivateBehaviour._Transform:
                            Debug.Log("Transform");
                            break;

                        case ActivateBehaviour._Destroy:
                            Destroy(connectedGameObject);
                            break;

                        case ActivateBehaviour._SetActive:
                            connectedGameObject.SetActive(true);
                            break;

                        }
                    }
                    break;

                case WaveType.PushCorruption:
                    if (canBePushCorrupted == true)
                    {
                        ParticleSystem ps = this.GetComponent<ParticleSystem>();
                        ps = myPSList[0];

                        ps.Play();
                    }
                    break;

                case WaveType.PullCorruption:
                    if (canBePullCorrupted == true)
                    {
                        ParticleSystem ps = this.GetComponent<ParticleSystem>();
                        ps = myPSList[1];

                        ps.Play();
                    }
                    break;

                case WaveType.ActivateCorruption:
                    if (canBeActivateCorrupted == true)
                    {
                        ParticleSystem ps = this.GetComponent<ParticleSystem>();
                        ps = myPSList[2];

                        ps.Play();
                    }
                    break;
            }

    }

}

public enum ActivateBehaviour {_Transform, _Destroy, _SetActive}