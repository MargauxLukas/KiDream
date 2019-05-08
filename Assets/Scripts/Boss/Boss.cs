using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator       animator;
    Animator  bombAnimator1;
    Animator  bombAnimator2;
    Animator  bombAnimator3;
    Animator cameraAnimator; //Pour lancer le screenShake

    [Header("GameObject to attached")]
    public GameObject          bomb;
    public GameObject         ombre; //Ombre Prefab
    public GameObject  bombLauncher; //Zone ou les bombes spawnent
    public GameObject pushCorrupted;
    public GameObject areaDetection;
    public GameObject  wallCollider;

    private GameObject ombreObject;  //GameObject apres l'instantiate de l'ombre
    private GameObject      bombe1;
    private GameObject      bombe2;
    private GameObject      bombe3;
    private GameObject      player;

    private Transform target;

    private Rigidbody2D rb;

    [Header("Boss Characteristics")]
    public float speed = 0.2f;

    private float timer            ;
    private float distanceDeltaHV  ;
    private float distanceDeltaDiag;

    public bool isRage = false;

    private bool bossFallDown    = false;
    private bool needToMove1     = false;
    private bool needToMove2     = false;
    private bool isStartingPhase =  true;
    private bool isDream = true;
    private bool isInvincible = false;
    private bool isFollow = true;
    private bool isBombR = true;
    private bool isLastPhase = true;

    private int seconds   = 0;
    private int lookingAt = 0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up
    private int direction = 0;
    public  int hp        = 5;

    void Start()
    {
        animator = GetComponent<Animator>();
        cameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        bombAnimator1 = null;
        bombAnimator1 = null;
        bombAnimator1 = null;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        target = player.transform;

        distanceDeltaHV = Time.deltaTime * 1f;
        distanceDeltaDiag = Time.deltaTime * 0.5f;
    }
	
	void Update ()
    {

        Debug.Log(GetComponent<ReactionToWave>().corruptedPushRadius);
        isDream = player.GetComponent<CharacterController>().isDream;
        if (isDream) { animator.SetBool("isDream",  true);}
        else         { animator.SetBool("isDream", false);}

        if (needToMove1 || needToMove2)                                            //Verification si bombe a atteint destination (1 variable pour bombes sur les côtés, 1 variable pour la bombe au centre)
        {
            MovingBomb();
            if (bombAnimator1 != null) {bombAnimator1.SetBool("isMoving", true);}
            if (bombAnimator2 != null) {bombAnimator2.SetBool("isMoving", true);}
            if (bombAnimator3 != null) {bombAnimator3.SetBool("isMoving", true);}
        }
        else if (!needToMove1 && !needToMove2)
        {
            if (bombAnimator1 != null) {bombAnimator1.SetBool("isMoving", false);}
            if (bombAnimator2 != null) {bombAnimator2.SetBool("isMoving", false);}
            if (bombAnimator3 != null) {bombAnimator3.SetBool("isMoving", false);}
        }

        if (Time.time > timer + 1) //Timer
        {
            timer = Time.time;
            seconds++;
        }

        /*******************************************
         *          DEROULEMENT DES PHASES         *
         *  HP du boss lié au changement de phase  *
         *   A augmenter si on veut plus de phase  *
         *******************************************/
        if (hp == 5) //Phase 1 
        {
            if (seconds >= 1 && seconds < 5) // Fight commence à 1 seconde pour pas que le boss bouge direct apres la cinematique
            {
                animator.SetBool("isMoving", true);
                Move();
            }
            if(seconds == 5)
            {
                animator.SetBool("isLaunching", true);
            }
            if (seconds == 6)
            {
                animator.SetBool(   "isMoving", false);
                direction = ThrowBomb(lookingAt)      ;
                seconds   = -5                        ;       //A augmenter si on veut que le boss bouge plus rapidement après avoir balancé ses bombes
                animator.SetBool("isLaunching", false);
            }
        }

        if(hp == 4)                                           //Phase 2
        {
            if (isStartingPhase)                              //Changement de phase
            {
                if (!bossFallDown)                            // Sauter
                {
                    animator.SetBool("isJumping",  true);
                    Jump(2);
                }
                else                                          //Atterir
                {
                    animator.SetBool("isJumping", false);
                    BossLanding();
                }
            }
            else
            {
                speed = 0.4f;                                 //Vitesse du boss augmenté
                if (seconds < 3)
                {
                    animator.SetBool("isMoving", true);
                    cameraAnimator.ResetTrigger("shake");
                    Move();
                }
                if(seconds == 3)
                {
                    animator.SetBool("isLaunching", true);
                }
                if (seconds == 4)
                {
                    cameraAnimator.SetBool("isTrigger", false);
                    animator.SetBool(   "isMoving", false);
                    direction = ThrowBomb(lookingAt)      ;
                    seconds   = 0                         ;
                    animator.SetBool("isLaunching", false);
                }
            }
        }

        if (hp == 3)                                          //Phase 3
        {
            if (isStartingPhase)
            {
                if (!bossFallDown)                            //Sauter
                {
                    animator.SetBool("isJumping", true);
                    Jump(3);
                }
                else                                          //Atterir
                {
                    animator.SetBool("isJumping", false);
                    BossLanding();
                }
            }
            else
            {  
                speed = 0.5f;                                 //Vitesse du boss augmenté
                if (seconds < 2)
                {
                    animator.SetBool("isMoving", true);
                    bossFallDown = false;
                    cameraAnimator.ResetTrigger("shake");
                    Move();
                }
                if(seconds == 2)
                {
                    animator.SetBool("isLaunching", true);
                }
                if (seconds == 3)
                {
                    cameraAnimator.SetBool("isTrigger", false);
                    animator.SetBool(  "isMoving", false);
                    direction = ThrowBomb(lookingAt);
                    seconds = 0;
                    animator.SetBool("isLaunching", false);
                }
            }
        }

        if (hp == 2)                                          //Phase 4
        {
            if (isStartingPhase)
            {
                if (!bossFallDown)                            //Sauter
                {
                    animator.SetBool("isJumping", true);
                    Jump(4);
                }
                else                                          //Atterir
                {
                    animator.SetBool("isJumping", false);
                    BossLanding();
                }
            }
            else
            {
                speed = 0.6f;                                 //Vitesse du boss augmenté
                if (seconds < 2)
                {
                    animator.SetBool("isMoving", true);
                    bossFallDown = false;
                    cameraAnimator.ResetTrigger("shake");
                    Move();
                }
                if(seconds == 2)
                {
                    animator.SetBool("isLaunching", true);
                }
                if (seconds == 3)
                {
                    cameraAnimator.SetBool("isTrigger", false);
                    animator.SetBool("isMoving", false);
                    direction = ThrowBomb(lookingAt);
                    seconds = 0;
                    animator.SetBool("isLaunching", false);
                }
            }
        }
        if (hp == 1)                                          //Phase 4
        {
            if (isStartingPhase)
            {
                if (!bossFallDown)                            //Sauter
                {
                    animator.SetBool("isJumping", true);
                    Jump(5);
                }
                else                                          //Atterir
                {
                    animator.SetBool("isJumping", false);
                    BossLanding();
                    seconds = 0;
                }
            }
            else
            {
                GetComponent<BombAOE>().isPlayed3 = false;
                isRage = true;
                Destroy(wallCollider);

                if(isLastPhase)
                {
                    LastPhaseBomb();
                }
                if (isBombR)
                {
                    StartCoroutine(BombRandom());
                    isBombR = false;
                }
                if (seconds > 3)
                {
                    bossFallDown = false;
                    cameraAnimator.ResetTrigger("shake");
                }
                if(seconds == 4)
                {
                    cameraAnimator.SetBool("isTrigger", false);
                }
                if(seconds == 5)
                {
                    isStartingPhase = true;
                    isLastPhase = true;
                }
            }
        }
    }


   /*******************************************************************************
   * Function : Le boss suit du regard le joueur et sauvegarde vers ou il regarde *
   ********************************************************************************/
    void LookAt()
    {
        float xDifference = Mathf.Abs(transform.position.x - target.position.x);
        float yDifference = Mathf.Abs(transform.position.y - target.position.y);

        if (xDifference > yDifference)
        {
            animator.SetFloat("moveY", 0f);

            if (target.position.x > transform.position.x)
            {
                animator.SetFloat("moveX",  1f);
                lookingAt = 1                  ;                 //Droite
            }
            if (target.position.x < transform.position.x)
            {
                animator.SetFloat("moveX", -1f);
                lookingAt = 3                  ;                 //Gauche
            }
        }
        else if (xDifference < yDifference)
        {
            animator.SetFloat("moveX", 0f);

            if (target.position.y < transform.position.y)
            {
                animator.SetFloat("moveY", -1f);
                lookingAt = 2                  ;                 //Bas
            }
            if (target.position.y > transform.position.y)
            {
                animator.SetFloat("moveY",  1f);
                lookingAt = 4                  ;                 //Haut
            }
        }
    }

    /***********************************************
    * Function : Le boss se déplace vers le joueur *
    ************************************************/
    void Move()
    {
        //StartCoroutine(areaDetectionTrue());
        LookAt();
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    IEnumerator areaDetectionTrue()
    {
        pushCorrupted.GetComponent<CircleCollider2D>().radius = 0.6f;
        yield return new WaitForSeconds(1f);
        areaDetection.SetActive(true);
    }

    /****************************
    * Function : Le boss saute  *
    *****************************/
    void Jump(int phase)
    {
        //pushCorrupted.GetComponent<CircleCollider2D>().radius = 2f;
        StartCoroutine(WaitJump(phase));
    }

    IEnumerator WaitJump(int phase)
    {
        if (phase == 2)
        {
            yield return new WaitForSeconds(0.9f);
            gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 4.5f), 0.1f);
            yield return new WaitForSeconds(0.2f);
            Shadow(2); //Ombre à Instantiate
        }
        else if(phase == 3)
        {
            yield return new WaitForSeconds(0.9f);
            gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 4.5f), 0.1f);
            yield return new WaitForSeconds(0.2f);
            Shadow(3); //Ombre à Instantiate
            InstantiateBomb(phase);  // Bombe qu'il lâche en l'air
        }
        else if (phase == 4)
        {
            yield return new WaitForSeconds(0.9f);
            gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 4.5f), 0.1f);
            yield return new WaitForSeconds(0.2f);
            Shadow(4); //Ombre à Instantiate
            InstantiateBomb(phase);
        }
        else if (phase == 5)
        {
            yield return new WaitForSeconds(0.9f);
            gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 4.5f), 0.1f);
            yield return new WaitForSeconds(0.2f);
            Shadow(5); //Ombre à Instantiate
            InstantiateBomb(phase);
        }
    }

    /**************************************************************************************
    * Function : L'ombre du boss apparait lorsqu'il est hors de l'écran et suit le joueur *
    ***************************************************************************************/
    void Shadow(int phase) //A améliorer, Moyen de réduire (Switch case)
    {
        if (phase == 2)
        {
            if (ombreObject == null) { ombreObject = Instantiate(ombre, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3.50f, 0), Quaternion.identity); }    //Instantiate Ombre 

            ombreObject.transform.position = Vector2.MoveTowards(ombreObject.transform.position, target.position, 0.5f * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(ombreObject.transform.position.x, transform.position.y), 1f * Time.deltaTime);

            StartCoroutine(WaitShadow(phase));
        }
        else if (phase == 3)
        {
            if (ombreObject == null) { ombreObject = Instantiate(ombre, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3.50f, 0), Quaternion.identity); }    //Instantiate Ombre 

            ombreObject.transform.position = Vector2.MoveTowards(ombreObject.transform.position, target.position, 0.5f * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(ombreObject.transform.position.x, transform.position.y), 1f * Time.deltaTime);
        }
        else if (phase == 4)
        {
            if (ombreObject == null) { ombreObject = Instantiate(ombre, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3.50f, 0), Quaternion.identity); }    //Instantiate Ombre 

            if (isFollow)
            {
                ombreObject.transform.position = Vector2.MoveTowards(ombreObject.transform.position, target.position, 0.5f * Time.deltaTime);
            }
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(ombreObject.transform.position.x, transform.position.y), 1f * Time.deltaTime);

            StartCoroutine(WaitShadow(phase));
        }
        else if (phase == 5)
        {
            if (ombreObject == null) { ombreObject = Instantiate(ombre, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3.50f, 0), Quaternion.identity); }    //Instantiate Ombre 

            ombreObject.transform.position = Vector2.MoveTowards(ombreObject.transform.position, new Vector2(0f,1f), 0.5f * Time.deltaTime);

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(ombreObject.transform.position.x, transform.position.y), 1f * Time.deltaTime);

            StartCoroutine(WaitShadow(phase));
        }
    }

    IEnumerator WaitShadow(int phase)
    {
        if (phase == 2 || phase == 3)
        {
            yield return new WaitForSeconds(2f);
            bossFallDown = true;
        }
        else if (phase == 4)
        {
            yield return new WaitForSeconds(2f);
            isFollow = false;
            if (target.position.x > 0 && ombreObject != null) { ombreObject.transform.position = Vector2.MoveTowards(ombreObject.transform.position, new Vector2(-0.7f, 0.50f), 1f * Time.deltaTime); }
            else
            {
                if (ombreObject != null)
                {
                    ombreObject.transform.position = Vector2.MoveTowards(ombreObject.transform.position, new Vector2(1f, 0.50f), 1f * Time.deltaTime);
                }
            }
            yield return new WaitForSeconds(1.5f);
            bossFallDown = true;
        }
        else if (phase == 5)
        {
            yield return new WaitForSeconds(1f);
            bossFallDown = true;
        }
    }

    /*************************************************************************************************************
    * Function : Le boss retombe apres avoir sauté et détruit les pilliers et tue le joueur si il est en dessous *
    **************************************************************************************************************/
    void BossLanding()
    {
        if (ombreObject != null) { gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, ombreObject.transform.position.y), 0.1f); }
        animator.SetBool("isLanding", true);

        StartCoroutine(WaitLanding());
    }

    IEnumerator WaitLanding()
    {
        yield return new WaitForSeconds(1.30f);
        cameraAnimator.SetTrigger("shake")    ;
        cameraAnimator.SetBool("isTrigger", true);
        Destroy(ombreObject)                  ;
        PushWave()    ;
        //Detruit pillier et repousse bombe

        yield return new WaitForSeconds(0.5f);
        //pushCorrupted.SetActive(false);
        animator.SetBool("isLanding", false) ;
        seconds = 0                          ;
        isStartingPhase = false              ;
        bossFallDown    = false              ;
    }

    void InstantiateBomb(int phase)
    {
        if (phase == 3)
        {
            if (player.transform.position.x < 0) { GetComponent<BombAOE>().BombAreaLeftToRight(); }  //Bombe laché de la gauche vers la droite
            if (player.transform.position.x > 0) { GetComponent<BombAOE>().BombAreaRightToLeft(); }  //Bombe laché de la droite vers la gauche
            StartCoroutine(WaitBomb());
        }
        else if (phase == 4)
        {
            GetComponent<BombAOE>().BombAreaMiddle();
        }
        else if (phase == 5)
        {
            GetComponent<BombAOE>().BombAreaCenter();
        }
    }

    IEnumerator WaitBomb()
    {
        yield return new WaitForSeconds(5f); //Temps avant que le boss retombe
        bossFallDown = true;
    }

    /********************************************************************************************
    * Function : Le boss passe en rage, il saute et en atterissant détruit les murs de la salle *
    *********************************************************************************************/
    void Rage()
    {
        isRage = true;
    }

    IEnumerator BombRandom()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<BombAOE>().BombAreaRandom();
        StartCoroutine(BombRandom());
    }

    void LastPhaseBomb()
    {
        isLastPhase = false;
        GetComponent<BombAOE>().LastBomb();
    }

    /****************************************************************
    * Function : Si le joueur est trop prêt du boss, il le repousse *
    *****************************************************************/
    public void PushPlayerOn()
    {
        
    }

    public void PushPlayerOff()
    {
        
    }

    /***************************************************
    * Function : Le boss balance les bombes devant lui *
    ****************************************************/
    int ThrowBomb(int lookingAt)
    {
        areaDetection.SetActive(false);
        
        switch(lookingAt)
        {
            case 1: //Est
                bombe1        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombe2        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombe3        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombAnimator1 = bombe1.GetComponent<Animator>();
                bombAnimator2 = bombe2.GetComponent<Animator>();
                bombAnimator3 = bombe3.GetComponent<Animator>();
                needToMove1   = true;
                needToMove2   = true;
                return 1;

            case 2: //Sud
                bombe1        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombe2        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombe3        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombAnimator1 = bombe1.GetComponent<Animator>();
                bombAnimator2 = bombe2.GetComponent<Animator>();
                bombAnimator3 = bombe3.GetComponent<Animator>();
                needToMove1   = true;
                needToMove2   = true;
                return 2;

            case 3: //Ouest
                bombe1        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombe2        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombe3        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombAnimator1 = bombe1.GetComponent<Animator>();
                bombAnimator2 = bombe2.GetComponent<Animator>();
                bombAnimator3 = bombe3.GetComponent<Animator>();
                needToMove1   = true;
                needToMove2   = true;
                return 3;

            case 4: //Nord
                bombe1        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombe2        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombe3        = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y), Quaternion.identity);
                bombAnimator1 = bombe1.GetComponent<Animator>();
                bombAnimator2 = bombe2.GetComponent<Animator>();
                bombAnimator3 = bombe3.GetComponent<Animator>();
                needToMove1   = true;
                needToMove2   = true;
                return 4;

            default:
                return 0;
        }
    }

    /********************************************************************************
     * Function : Si Bombe, bouger la bombe jusqu'à qu'elle atteigne sa destination *
     ********************************************************************************/
    void MovingBomb() //Il faudrait rajouter une condition si la bombe est touché par particule avant d'atteindre sa destination et moyen de faire un switch case
    {
        if (direction != 0 && bombe1 != null 
                           && bombe2 != null 
                           && bombe3 != null)
        {
            if (direction == 1) //EST
            {
                //Debug.Log("Bombe en Est");
                float xDiffBomb1 = Mathf.Abs(transform.position.x - bombe1.transform.position.x); //Bombe sur les côtés
                float xDiffBomb2 = Mathf.Abs(transform.position.x - bombe2.transform.position.x); //Bombe du millieu

                if (xDiffBomb1 < 0.60f) //Diagonale
                {
                    bombe1.transform.Translate(transform.up *  distanceDeltaDiag + transform.right * distanceDeltaDiag);
                    bombe3.transform.Translate(-transform.up * distanceDeltaDiag + transform.right * distanceDeltaDiag);
                }
                else {needToMove1 = false;}

                if (xDiffBomb2 < 1.5f) {bombe2.transform.position += transform.right * distanceDeltaHV;}
                else                   {needToMove2 = false                                           ;}
            }
            else if (direction == 2) //SUD
            {
                //Debug.Log("Bombe en Sud");

                float yDiffBomb1 = Mathf.Abs(transform.position.y - bombe1.transform.position.y); //Bombe sur les côtés
                float yDiffBomb2 = Mathf.Abs(transform.position.y - bombe2.transform.position.y); //Bombe du millieu

                if (yDiffBomb1 < 1f) //Diagonale
                {
                    bombe1.transform.Translate(-transform.up * distanceDeltaDiag +  transform.right * distanceDeltaDiag);
                    bombe3.transform.Translate(-transform.up * distanceDeltaDiag + -transform.right * distanceDeltaDiag);
                }
                else{needToMove1 = false;}

                if (yDiffBomb2 < 2f) {bombe2.transform.position += -transform.up * distanceDeltaHV;}
                else                 {needToMove2 = false                                         ;}
            }
            else if (direction == 3) //OUEST
            {
                //Debug.Log("Bombe en Ouest");

                float xDiffBomb1 = Mathf.Abs(transform.position.x - bombe1.transform.position.x); //Bombe sur les côtés
                float xDiffBomb2 = Mathf.Abs(transform.position.x - bombe2.transform.position.x); //Bombe du millieu

                if (xDiffBomb1 < 1f) //Diagonale
                {
                    bombe1.transform.Translate( transform.up * distanceDeltaDiag + -transform.right * distanceDeltaDiag);
                    bombe3.transform.Translate(-transform.up * distanceDeltaDiag + -transform.right * distanceDeltaDiag);
                }
                else{needToMove1 = false;}

                if (xDiffBomb2 < 2f) {bombe2.transform.position += -transform.right * distanceDeltaHV;}
                else                 {needToMove2 = false                                            ;}
            }
            else if (direction == 4) //NORD
            {
                //Debug.Log("Bombe en Nord");

                float yDiffBomb1 = Mathf.Abs(transform.position.y - bombe1.transform.position.y); //Bombe sur les côtés
                float yDiffBomb2 = Mathf.Abs(transform.position.y - bombe2.transform.position.y); //Bombe du millieu

                if (yDiffBomb1 < 1f) //Diagonale
                {
                    bombe1.transform.Translate(transform.up * distanceDeltaDiag +  transform.right * distanceDeltaDiag);
                    bombe3.transform.Translate(transform.up * distanceDeltaDiag + -transform.right * distanceDeltaDiag);
                }
                else{needToMove1 = false;}

                if (yDiffBomb2 < 2f) {bombe2.transform.position += transform.up * distanceDeltaHV;}
                else                 {needToMove2 = false                                        ;}
            }
            bombAnimator1.SetInteger("direction", direction);
            bombAnimator2.SetInteger("direction", direction);
            bombAnimator3.SetInteger("direction", direction);
        }
    }

    /***********************************************************************************************
     * Function : Le boss fait apparaitre des mob clochettes, ils se déplacent et peuvent exploser *
     ***********************************************************************************************/
    void MobInvoking()
    {
        //Invoque les petits mobs clochettes "Grelottins".
    }

    /****************************************************************
     * Function : Onde qui repousse le joueur, les IA et les objets *
     ****************************************************************/
    public void PushWave()
    {
        StartCoroutine(PushWaveC());
    }

    IEnumerator PushWaveC()
    {
        pushCorrupted.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        pushCorrupted.SetActive(false);
    }
    /**************************************************************
     * Function : Le boss prend un dégat quand touché par bombes  *
     **************************************************************/
    public void Damages()
    {
        if(!isInvincible)
        {
            hp--;
            isStartingPhase = true;
            isInvincible    = true;
        }

        StartCoroutine(Invincible());

        if(hp == 0){isDead();}
    }

     /**************************************************************
     * Function : Boss invincible un temps après l'avoir touché    *
     **************************************************************/
    IEnumerator Invincible()
    {
        yield return new WaitForSeconds(2f);
        isInvincible = false;
    }

    /*****************************
     * Function : Le boss meurt  *
     *****************************/
    void isDead()
    {
        //Play Death of Mister Cloclo
    }
}
