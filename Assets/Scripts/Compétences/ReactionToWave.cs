using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionToWave : MonoBehaviour
{

    public List<ParticleSystem> myPSList = new List<ParticleSystem>();

    private ParticleSystem shooter;
    private int localCounter;
    private Rigidbody2D thisRb;
    private ParticleSystem ps;

    public WaveManager waveManager;

    [Header("Physics")]
    [Range(0,10), SerializeField]
    private int linearDrag;

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
    public bool playAnimation;
    public bool isActivated;
    public ActivateBehaviour activateBehaviour;

    [Header("Enfants")]
    public int childNumberTolerance;

    private bool doubleLock = false;



	// Start
	void Start()
    {
        thisRb = this.GetComponent<Rigidbody2D>();
        thisRb.drag = linearDrag;
	}
	
	// Update
	void Update()
    {
        ActivationDesactivationAnimation();
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
                    if(canBeActivated == true)
                    {

                        /*if (isActivated == true)
                        {
                            isActivated = false;
                        }
                        else
                        {
                            isActivated = true;
                        }*/
                        

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
                    if(canBePushCorrupted == true)
                    {
                        SetupChosenParticleSystem();
                    }
                    break;

                case WaveType.PullCorruption:

                    localCounter = 1;
                    if(canBePullCorrupted == true)
                    {
                        SetupChosenParticleSystem();
                    }
                    break;

                case WaveType.ActivateCorruption:

                    localCounter = 2;
                    if(canBeActivateCorrupted == true)
                    {
                        SetupChosenParticleSystem();
                    }
                    break;
            }

    }

    public void SetupChosenParticleSystem()
    {    

        if (this.transform.childCount == childNumberTolerance)
        {
            ps = Instantiate(myPSList[localCounter]);
            ps.transform.position = this.transform.position;
            ps.gameObject.SetActive(true);
            ps.transform.SetParent(this.transform);
            ps.Play();
        }
        else if (this.transform.childCount > childNumberTolerance)
        {
            Transform child = this.transform.GetChild(childNumberTolerance);
            Destroy(child.gameObject);
            child.parent = null;
            SetupChosenParticleSystem();
        }
    }

    public void ActivationDesactivationAnimation()
    {
        if(playAnimation == true)
        {
            if (isActivated == true && doubleLock == false)
            {
                Animator myAnim = this.GetComponent<Animator>();
                myAnim.SetBool("isActivated", true);
                doubleLock = true;
            }
            else if (isActivated == false && doubleLock == true)
            {
                Animator myAnim = this.GetComponent<Animator>();
                myAnim.SetBool("isActivated", false);
                doubleLock = false;
            }
        }
    }

}

public enum ActivateBehaviour {_Transform, _Destroy, _SetActive}