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

    [Header("Properties")]
    public List<GameObject> whoCanShootMe = new List<GameObject>();
    public bool canBePushed = false;
    public bool canBePulled = false;
    public bool canBeActivated = false;
    public bool canBePushCorrupted = false;
    public bool canBePullCorrupted = false;
    public bool canBeActivateCorrupted = false;
    
    [Header("Push options")]
    [Range(0, 50), SerializeField]
    public float verticalPushForce = 1f;
    [Range(0, 50), SerializeField]
    public float horizontalPushForce = 1f;

    [Header("Pull options")]
    [Range(0, 50), SerializeField]
    public float verticalPullForce = 1f;
    [Range(0, 50), SerializeField]
    public float horizontalPullForce = 1f;

    [Header("Activate options")]
    public GameObject connectedGameObject;
    public bool playAnimation;
    public bool launchAgainToDisable;
    public bool isActivated;
    public ActivateBehaviour activateBehaviour;
    public bool setActiveSetting;

    [Header("Corrupted Push options")]
    [Range(0, 2), SerializeField]
    public float corruptedPushRadius;

    [Header("Corrupted Pull options")]
    [Range(0, 2), SerializeField]
    public float corruptedPullRadius;

    [Header("Corrupted Activate options")]
    [Range(0, 2), SerializeField]
    public float corruptedActivateRadius;

    [Header("Child")]
    public int childNumberTolerance;

    [Header("Bypass")]
    public bool bypassActivateRules;

    private bool doubleLock = false;

    // Start
    void Start()
    {
        CrashAvoider();

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
         foreach (GameObject go in whoCanShootMe)
         {
             if (other == go || (other.transform.parent != null && other.transform.parent.gameObject == go))
             {
                Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
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
                         if(canBePushed == true)
                         {
                             rb.AddForce(new Vector2(-(this.shooter.transform.position.x - this.transform.position.x) * horizontalPushForce, -(this.shooter.transform.position.y - this.transform.position.y) * verticalPushForce));
                         }
                         break;

                     case WaveType.Pull:
                         if(canBePulled == true)
                         {
                             rb.AddForce(new Vector2((this.shooter.transform.position.x - this.transform.position.x) * horizontalPullForce, (this.shooter.transform.position.y - this.transform.position.y) * verticalPullForce));
                         }
                         break;

                     case WaveType.Activate:
                         if(canBeActivated == true)
                         {
                             Activate();
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
            else if (other != go || (other.transform.parent == null && other.transform.parent.gameObject != go))
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

                ParticleList[indexParticle].remainingLifetime = ParticleList[indexParticle].remainingLifetime +1f;

                shooter.SetParticles(ParticleList, shooter.particleCount);
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

    public void CrashAvoider()
    {
        if(this.GetComponent<Animator>() == null)
        {
            playAnimation = false;
        }
    }

    public void BypassActivationRules()
    {
        if(bypassActivateRules == true)
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
            Debug.Log("isActivated = true");
            isActivated = true;

            switch (activateBehaviour)
            {
                case ActivateBehaviour._Debug:
                    Debug.Log("Debug Message");
                    break;

                case ActivateBehaviour._Destroy:
                    Destroy(connectedGameObject);
                    break;

                case ActivateBehaviour._SetActive:
                    connectedGameObject.SetActive(setActiveSetting);
                    break;
            }
        }
    }
}

public enum ActivateBehaviour {_Debug, _Destroy, _SetActive}