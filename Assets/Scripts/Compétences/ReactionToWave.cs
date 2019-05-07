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

    private WaveManager waveManager;

    [Header("Physics")]
    [Range(0, 10), SerializeField]
    private int linearDrag = 1;

    [Header("Properties")]
    public List<GameObject> whoCanShootMe = new List<GameObject>();
    public bool canBePushed = false;
    public bool canBePulled = false;
    public bool canBeActivated = false;
    public bool canBePushCorrupted = false;
    public bool canBePullCorrupted = false;
    public bool canBeActivateCorrupted = false;

    [Header("Push options")]
    [Range(0, 500), SerializeField]
    public float verticalPushForce = 1f;
    [Range(0, 500), SerializeField]
    public float horizontalPushForce = 1f;
    public bool pushXEqualY;

    [Header("Pull options")]
    [Range(0, 500), SerializeField]
    public float verticalPullForce = 1f;
    [Range(0, 500), SerializeField]
    public float horizontalPullForce = 1f;
    public bool pullXEqualY;

    [Header("Activate options")]
    public GameObject connectedGameObject;
    public bool playAnimation;
    public bool launchAgainToDisable;
    public bool isActivated;
    public ActivateBehaviour activateBehaviour;

    [Header("Corrupted options")]
    [Range(0, 2), SerializeField]
    public float corruptedPushRadius;
    [Range(0, 2), SerializeField]
    public float corruptedPullRadius;
    [Range(0, 2), SerializeField]
    public float corruptedActivateRadius;

    [Header("Bypass")]
    public bool bypassActivateRules;

    private bool doubleLock = false;
    private int childNumberTolerance;

    // Start
    void Start()
    {
        CrashAvoider();

        waveManager = FindObjectOfType<WaveManager>();

        myPSList.Clear();

        GameObject psReferencer = GameObject.Find("ParticleSystemReferencer");
        ParticleSystem[] array = psReferencer.GetComponentsInChildren<ParticleSystem>(true);

        foreach(ParticleSystem ps in array)
        {
            myPSList.Add(ps);
        }

        childNumberTolerance = this.transform.childCount;
    }


    // Update
    void Update()
    {
        ActivationDesactivationAnimation();

        if (pushXEqualY == true)
        {
            horizontalPushForce = verticalPushForce;
        }

        if (pullXEqualY == true)
        {
            horizontalPullForce = verticalPullForce;
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        foreach (GameObject go in whoCanShootMe)
        {
            if (other == go || (other.transform.parent != null && other.transform.parent.gameObject == go))
            {
                Debug.Log("ahah");
                shooter = other.GetComponent<ParticleSystem>();

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

                ParticleList[indexParticle].startLifetime = 0f;

                shooter.SetParticles(ParticleList, shooter.particleCount);

                BypassActivationRules();

                switch (waveManager.waveSelection)
                {
                    case WaveType.Push:
                        if (canBePushed == true)
                        {
                            thisRb.AddForce(new Vector2(-(this.shooter.transform.position.x - this.transform.position.x) * horizontalPushForce, -(this.shooter.transform.position.y - this.transform.position.y) * verticalPushForce));
                        }
                        break;

                    case WaveType.Pull:
                        if (canBePulled == true)
                        {
                            thisRb.AddForce(new Vector2((this.shooter.transform.position.x - this.transform.position.x) * horizontalPullForce, (this.shooter.transform.position.y - this.transform.position.y) * verticalPullForce));
                        }
                        break;

                    case WaveType.Activate:
                        if (canBeActivated == true)
                        {
                            Activate();
                        }
                        break;

                    case WaveType.PushCorruption:

                        localCounter = 0;
                        if (canBePushCorrupted == true)
                        {
                            SetupChosenParticleSystem();
                        }
                        break;

                    case WaveType.PullCorruption:

                        localCounter = 1;
                        if (canBePullCorrupted == true)
                        {
                            SetupChosenParticleSystem();
                        }
                        break;

                    case WaveType.ActivateCorruption:

                        localCounter = 2;
                        if (canBeActivateCorrupted == true)
                        {
                            SetupChosenParticleSystem();
                        }
                        break;
                }

            }
            else if (other != go || (other.transform.parent == null && other.transform.parent.gameObject != go))
            {
                if(other.GetComponent<ParticleSystem>() != null)
                {
                    shooter = other.GetComponent<ParticleSystem>();

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

                    ParticleList[indexParticle].remainingLifetime = ParticleList[indexParticle].remainingLifetime + 1f;

                    shooter.SetParticles(ParticleList, shooter.particleCount);
                }
            }


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

            switch (localCounter)
            {
                case 0:
                    ps.startSize = 2.76131f * corruptedPushRadius + 0.063143f;
                    break;

                case 1:
                    ps.startSize = 2.76131f * corruptedPullRadius + 0.063143f;
                    break;

                case 2:
                    ps.startSize = 2.76131f * corruptedActivateRadius + 0.063143f;
                    break;
            }

            ps.Play();
            ps.Stop();
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
        if (playAnimation == true)
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

    public void CrashAvoider()
    {
        if (this.GetComponent<Animator>() == null)
        {
            playAnimation = false;
        }

        if (this.GetComponent<Rigidbody2D>() != null)
        {
            thisRb = this.GetComponent<Rigidbody2D>();
            thisRb.drag = linearDrag;
        }
        else
        {
            Debug.Log(this.gameObject.name + " n'a pas de RigidBody2D attaché");
            canBePushed = false;
            canBePulled = false;
        }
    }

    public void BypassActivationRules()
    {
        if (bypassActivateRules == true)
        {
            Activate();
            return;
        }
    }

    public void Activate()
    {
        if (isActivated == true && launchAgainToDisable == true)
        {
            isActivated = false;
        }
        else if (isActivated != true)
        {
            switch (activateBehaviour)
            {
                case ActivateBehaviour._Debug:
                    break;

                case ActivateBehaviour._Destroy:
                    if(connectedGameObject != null)
                    {
                        Destroy(connectedGameObject);
                    }
                    break;

                case ActivateBehaviour._SetActiveTrue:
                    if (connectedGameObject != null)
                    {
                        connectedGameObject.SetActive(true);
                    }
                    break;

                case ActivateBehaviour._SetActiveFalse:
                    if (connectedGameObject != null)
                    {
                        connectedGameObject.SetActive(false);
                    }
                    break;

                case ActivateBehaviour._Shoot:
                    if (connectedGameObject != null)
                    {
                        connectedGameObject.GetComponent<ParticleSystem>().Play();
                    }
                    break;
            }

            isActivated = true;
        }
    }

    //Set colliding props to static
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ActionObject")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            rb.bodyType = RigidbodyType2D.Static;
        }
    }*/
}

public enum ActivateBehaviour {_Debug, _Destroy, _SetActiveTrue, _SetActiveFalse, _Shoot}