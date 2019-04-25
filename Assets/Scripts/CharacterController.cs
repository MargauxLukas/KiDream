using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 2f;

    private float moveX = 0f;
    private float moveY = 0f;
    private float nightmare = 0f;
    private float dream = 0f;

    public int hp = 3;
    public int damage = 1;
    public bool dialogueHasStarted = false;
    public bool reve = true;
    public bool isKilled = false;

    public int pushPower = 2;
    public int weight = 6;

    private Rigidbody2D rigidBody;
    private GameMaster gameMaster;

    Animator animator;
    DialogueManager dialogueManager;
    DialogueTrigger dialogueTrigger;
    Slider sliderHP;

    public GameObject tilemapD;
    public GameObject tilemapN;
    public GameObject dialogueManagerObject;
    public GameObject dialogueTriggerObject;
    public GameObject mySliderHP;

    private Collider2D[] hitResult = new Collider2D[10];

    //Lié a Ability1
    public GameObject seeAbility;
    public ParticleSystem notSeeAbility;

    //Liste des objets qu'il y'a en Cauchemar et Reve
    private GameObject[] cauchemarObjects;
    private GameObject[] reveObjects;

    private void Awake()
    {
        GoToDream();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        sliderHP = mySliderHP.GetComponent<Slider>();
        //tilemapD = GameObject.FindGameObjectWithTag("Reve");
        //tilemapN = GameObject.FindGameObjectWithTag("Reve");
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        nightmare = Input.GetAxis("GoToNightmare");
        dream = Input.GetAxis("GoToDream");

        //sliderHP.value = hp;

        if(Input.GetKeyDown(KeyCode.Joystick1Button0) && dialogueHasStarted == false)
        {
            dialogueTrigger = dialogueTriggerObject.GetComponent<DialogueTrigger>();
            dialogueTrigger.TriggerDialogue();
            dialogueHasStarted = true;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button0) && dialogueHasStarted == true)
        {
            dialogueManager = dialogueManagerObject.GetComponent<DialogueManager>();
            dialogueManager.DisplayNextSentence();
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(maxSpeed * moveX, maxSpeed * moveY);

        IsMoving();
        IsAttacking();
        Ability1();
        isDead(hp);
        
        if(nightmare != 0)
        {
            GoToNightmare();
        }
        else if (dream != 0)
        {
            GoToDream();
        }
    }

    void IsMoving()
    {
        if ((moveX > 0 || moveX < 0) || (moveY > 0 || moveY < 0))
        {
            animator.SetFloat("moveX", moveX);
            animator.SetFloat("moveY", moveY);

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    /**************************************
     * Attaque Simple (A Retravailler)    *
     **************************************/
    void IsAttacking()
    {
        if (Input.GetKeyDown("joystick 1 button 0") || Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("isAttacking");
            int nbHitObjects = Physics2D.OverlapCircleNonAlloc(transform.position , 1.0f, hitResult);

            foreach (Collider2D hitObject in hitResult)
            {
                Debug.Log(hitObject);
                if (hitObject.gameObject.tag == "Enemy")
                {
                    Debug.Log(hitObject + " > Life : " + hitObject.gameObject.GetComponent<Ennemy>().hpEnemy);
                    hitObject.gameObject.GetComponent<Ennemy>().hpEnemy = hitObject.gameObject.GetComponent<Ennemy>().hpEnemy - damage;
                }
            }
        }
    }

    /**************************************
     * Permet d'aller en mode Cauchemar   *
     **************************************/
    void GoToNightmare()
    {
        //GameObject.Find("Main Camera").GetComponent<Rippleeffect>().RippleEff(transform, 10f, 1f);
        tilemapD.GetComponent<TilemapRenderer>().enabled=false;
        tilemapN.GetComponent<TilemapRenderer>().enabled = true;
        reve = false;

        reveObjects = GameObject.FindGameObjectsWithTag("CeQuiApparaitEnReve");
        cauchemarObjects = GameObject.FindGameObjectsWithTag("CeQuiApparaitEnCauchemar");

        foreach (GameObject reveObject in reveObjects)  // Pour chaque object avec le tag "CeQuiApparaitEnReve", je desactive le spriteRenderer et active le isTrigger
        {      
            if (reveObject.GetComponent<SpriteRenderer>() != null)
            {
                reveObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (reveObject.GetComponent<ParticleSystem>().isPlaying == true)
            {
                reveObject.GetComponent<ParticleSystem>().Stop();
            }
            if (reveObject.GetComponent<BoxCollider2D>() != null && reveObject.name != "CubeD" && reveObject.name.Contains("KEY") == false) // Faire une liste pour les exceptions (optimisation)
            {
                reveObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }

        foreach (GameObject cauchemarObject in cauchemarObjects) // Pour chaque object avec le tag "CeQuiApparaitEnCauchemar", j'active le spriteRenderer et desactive le isTrigger
        {
            if (cauchemarObject.GetComponent<SpriteRenderer>() != null)
            {
                cauchemarObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (cauchemarObject.GetComponent<ParticleSystem>().isPlaying == false)
            {
                cauchemarObject.GetComponent<ParticleSystem>().Play();
            }
            if (cauchemarObject.GetComponent<BoxCollider2D>() != null && cauchemarObject.name != "CubeN")
            {
                cauchemarObject.GetComponent<BoxCollider2D>().enabled = true;
            }

        }
    }

    /*********************************
     * Permet d'aller en mode Reve   *
     *********************************/
    void GoToDream()
    {
        //GameObject.Find("Main Camera").GetComponent<Rippleeffect>().RippleEff(transform, 10f, 1f);
        tilemapD.GetComponent<TilemapRenderer>().enabled = true;
        tilemapN.GetComponent<TilemapRenderer>().enabled = false;
        reve = true;

        reveObjects = GameObject.FindGameObjectsWithTag("CeQuiApparaitEnReve");
        cauchemarObjects = GameObject.FindGameObjectsWithTag("CeQuiApparaitEnCauchemar");

        foreach (GameObject cauchemarObject in cauchemarObjects)
        {
            
            if (cauchemarObject.GetComponent<SpriteRenderer>() != null) // Pour chaque object avec le tag "CeQuiApparaitEnCauchemar", je desactive le spriteRenderer et active le isTrigger
            {
                cauchemarObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (cauchemarObject.GetComponent<ParticleSystem>().isPlaying == true)
            {
                cauchemarObject.GetComponent<ParticleSystem>().Stop();
            }
            if (cauchemarObject.GetComponent<BoxCollider2D>() != null && cauchemarObject.name != "CubeN")
            {
                cauchemarObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }

        foreach (GameObject reveObject in reveObjects) // Pour chaque object avec le tag "CeQuiApparaitEnReve", j'active le spriteRenderer et desactive le isTrigger
        {   
            if (reveObject.GetComponent<SpriteRenderer>() != null)
            {
                reveObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (reveObject.GetComponent<ParticleSystem>().isPlaying == false)
            {
                reveObject.GetComponent<ParticleSystem>().Play();
            }
            if (reveObject.GetComponent<BoxCollider2D>() != null && reveObject.name != "CubeD")
            {
                reveObject.GetComponent<BoxCollider2D>().enabled = true;
            }

        }
    }

    /*********************************************
     * Condition de Mort (Animation a rajouter)  *
     *********************************************/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        if (collision.tag == "DeathZone")
        {
            transform.position = gameMaster.lastCheckpointPos;
            isKilled = true;
        }
    }

    void isDead(int hp)
    {
        if(hp <= 0)
        {
            transform.position = gameMaster.lastCheckpointPos;
            isKilled = true;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeathZone")
        {
            Destroy(gameObject,0.2f); //Enlever le delai lorsque que y'aura l'animation
        }
    }*/


    /***************************
     * Abilité du monde "Vue"  *
     ***************************/
    void Ability1()
    {
        //if (Input.GetKeyDown("joystick 1 button 1") && !reve) { Instantiate(seeAbility,    transform.position, Quaternion.identity);} //Place une zone écartant les particules 
        //if (Input.GetKeyDown("joystick 1 button 1") && reve)  { Instantiate(notSeeAbility, transform.position, Quaternion.identity);} //Place une zone qui garde les particules à l'intérieur (A travailler)       
    }
}
