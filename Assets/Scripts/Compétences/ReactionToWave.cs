using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionToWave : MonoBehaviour
{

    public List<ParticleSystem> myPSList = new List<ParticleSystem>();

    private ParticleSystem shooter;
    private int localCounter;

    private ParticleSystem ps;

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
    public float VerticalPull = 1f;
    [Range(0, 50), SerializeField]
    public float HorizontalPull = 1f;

    [Header("Activate options")]
    public GameObject connectedGameObject;
    public bool isActivated = false;
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

                    localCounter = 0;
                    SetupChosenParticleSystem();                    
                    break;

                case WaveType.PullCorruption:

                    localCounter = 1;
                    SetupChosenParticleSystem();
                    break;

                case WaveType.ActivateCorruption:

                    localCounter = 2;
                    SetupChosenParticleSystem();
                    break;
            }

    }

    public void SetupChosenParticleSystem()
    {
        if (canBePushCorrupted == true && this.transform.childCount == 0)
        {
            ps = Instantiate(myPSList[localCounter]);
            ps.transform.position = this.transform.position;
            ps.gameObject.SetActive(true);
            ps.transform.SetParent(this.transform);
            ps.Play();
        }
        else if (canBeActivateCorrupted == true && this.transform.childCount != 0)
        {
            Transform child = this.transform.GetChild(0);
            Destroy(child.gameObject);
            this.transform.DetachChildren();
            Debug.Log(this.transform.childCount);
            SetupChosenParticleSystem();
        }
    }

}

public enum ActivateBehaviour {_Transform, _Destroy, _SetActive}