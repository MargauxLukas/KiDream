using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator animator;
    Animator bombAnimator1;
    Animator bombAnimator2;
    Animator bombAnimator3;

    GameObject player;

    [Header("GameObject to attached")]
    public  GameObject         bomb;
    public  GameObject        ombre; //Ombre sur le sol quand le boss est en l'air
    public  GameObject bombLauncher; //Zone ou les bombes spawnent

    private GameObject ombreObject;  
    private GameObject      bombe1;
    private GameObject      bombe2;
    private GameObject      bombe3;

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

    private int seconds   = 0;
    private int lookingAt = 0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up
    private int direction = 0;
    private int hp        = 3;

    void Start ()
    {
        animator = GetComponent<Animator   >();
        rb       = GetComponent<Rigidbody2D>();
        target   = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.Find("Player");

        bombAnimator1 = null;
        bombAnimator1 = null;
        bombAnimator1 = null;

        distanceDeltaHV   = Time.deltaTime *   1f;
        distanceDeltaDiag = Time.deltaTime * 0.5f;
    }
	
	void Update ()
    {
        isDream = player.GetComponent<CharacterController>().isDream;
        if (isDream) { animator.SetBool("isDream",  true);}
        else         { animator.SetBool("isDream", false);}

        if (needToMove1 || needToMove2)
        {
            MovingBomb();
            bombAnimator1.SetBool("isMoving", true);
            bombAnimator2.SetBool("isMoving", true);
            bombAnimator3.SetBool("isMoving", true);
        } //Tant que bombe n'a pas atteint sa destination
        else if (!needToMove1 && !needToMove2 && (bombe1 != null || bombe2 != null || bombe3 != null))
        {
            bombAnimator1.SetBool("isMoving", false);
            bombAnimator2.SetBool("isMoving", false);
            bombAnimator3.SetBool("isMoving", false);
        }

        if (Time.time > timer + 1) //Timer
        {
            timer = Time.time;
            seconds++;
        }

        if (hp == 3) //Phase 1 
        {
            if (seconds >= 1) // Fight commence à 1 seconde pour pas que le boss bouge direct apres la cinematique
            {
                animator.SetBool("isMoving", true);
                Move();
            }
            if (seconds == 6)
            {
                animator.SetBool(   "isMoving", false);
                animator.SetBool("isLaunching",  true);
                direction = ThrowBomb(lookingAt)      ;
                seconds   = -5                        ;
                animator.SetBool("isLaunching", false);
            }
        }

        if(hp == 2) //Phase 2
        {
            if (isStartingPhase)
            {
                if (!bossFallDown)
                {
                    animator.SetBool("isJumping", true);
                    Jump();
                }
                else
                {
                    animator.SetBool("isJumping", false);
                    BossLanding();
                }
            }
            else
            {
                seconds = 0;

                if (seconds >= 1)
                {
                    animator.SetBool("isMoving", true);
                    Move();
                }
                if (seconds == 6)
                {
                    animator.SetBool(   "isMoving", false);
                    animator.SetBool("isLaunching",  true);
                    direction = ThrowBomb(lookingAt)      ;
                    seconds   = -5                        ;
                    animator.SetBool("isLaunching", false);
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
                lookingAt = 1                  ; //Droite
            }
            if (target.position.x < transform.position.x)
            {
                animator.SetFloat("moveX", -1f);
                lookingAt = 3                  ; //Gauche
            }
        }
        else if (xDifference < yDifference)
        {
            animator.SetFloat("moveX", 0f);
            if (target.position.y < transform.position.y)
            {
                animator.SetFloat("moveY", -1f);
                lookingAt = 2                  ; //Bas
            }
            if (target.position.y > transform.position.y)
            {
                animator.SetFloat("moveY",  1f);
                lookingAt = 4                  ; //Haut
            }
        }
    }

    /***********************************************
    * Function : Le boss se déplace vers le joueur *
    ************************************************/
    void Move()
    {
        LookAt();
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    /****************************
    * Function : Le boss saute  *
    *****************************/
    void Jump()
    {
        StartCoroutine(WaitJump());
        //Le boss saute et atteri détruisant les piliers autours.
    }

    IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(0.9f);
        gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 4.5f), 0.1f);
        yield return new WaitForSeconds(0.9f);
        Shadow();
    }

    /**************************************************************************************
    * Function : L'ombre du boss apparait lorsqu'il est hors de l'écran et suit le joueur *
    ***************************************************************************************/
    void Shadow()
    {
        if (ombreObject == null){ombreObject = Instantiate(ombre, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3.50f, 0), Quaternion.identity);}

        ombreObject.transform.position = Vector2.MoveTowards(ombreObject.transform.position, target.position                                                    , 1f * Time.deltaTime);
        transform.position             = Vector2.MoveTowards(transform.position            , new Vector2(ombreObject.transform.position.x, transform.position.y), 1f * Time.deltaTime);

        StartCoroutine(WaitShadow());
    }

    IEnumerator WaitShadow()
    {
        yield return new WaitForSeconds(2f);
        bossFallDown = true;
    }


    /*************************************************************************************************************
    * Function : Le boss retombe apres avoir sauté et détruit les pilliers et tue le joueur si il est en dessous *
    **************************************************************************************************************/
    void BossLanding()
    {
        gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, ombreObject.transform.position.y), 0.1f);
        StartCoroutine(WaitLanding());
    }

    IEnumerator WaitLanding()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isLanding", true);
        //Detruit pillier
    }

    /********************************************************************************************
    * Function : Le boss passe en rage, il saute et en atterissant détruit les murs de la salle *
    *********************************************************************************************/
    void Rage()
    {
        //Détruit tout les murs autours de la salle
        isRage = true;
    }

    /****************************************************************
    * Function : Si le joueur est trop prêt du boss, il le repousse *
    *****************************************************************/
    void PushPlayer()
    {
        //Push Player if is very trop pret
    }

    /***************************************************
    * Function : Le boss balance les bombes devant lui *
    ****************************************************/
    int ThrowBomb(int lookingAt)
    {
        switch(lookingAt)
        {
            case 1: //Est
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombAnimator1 = bombe1.GetComponent<Animator>();
                bombAnimator2 = bombe2.GetComponent<Animator>();
                bombAnimator3 = bombe3.GetComponent<Animator>();
                needToMove1 = true;
                needToMove2 = true;
                return 1;

            case 2: //Sud
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombAnimator1 = bombe1.GetComponent<Animator>();
                bombAnimator2 = bombe2.GetComponent<Animator>();
                bombAnimator3 = bombe3.GetComponent<Animator>();
                needToMove1 = true;
                needToMove2 = true;
                return 2;

            case 3: //Ouest
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombAnimator1 = bombe1.GetComponent<Animator>();
                bombAnimator2 = bombe2.GetComponent<Animator>();
                bombAnimator3 = bombe3.GetComponent<Animator>();
                needToMove1 = true;
                needToMove2 = true;
                return 3;

            case 4: //Nord
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombAnimator1 = bombe1.GetComponent<Animator>();
                bombAnimator2 = bombe2.GetComponent<Animator>();
                bombAnimator3 = bombe3.GetComponent<Animator>();
                needToMove1 = true;
                needToMove2 = true;
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
                else
                {
                    needToMove1 = false;
                }

                if (xDiffBomb2 < 1.5f) //Horizontale
                {
                    bombe2.transform.position += transform.right * distanceDeltaHV;
                }
                else
                {
                    needToMove2 = false;
                }
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
                else
                {
                    needToMove1 = false;
                }

                if (yDiffBomb2 < 2f) //Vertical
                {
                    bombe2.transform.position += -transform.up * distanceDeltaHV;
                }
                else
                {
                    needToMove2 = false;
                }
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
                else
                {
                    needToMove1 = false;
                }

                if (xDiffBomb2 < 2f) //Horizontale
                {
                    bombe2.transform.position += -transform.right * distanceDeltaHV;
                }
                else
                {
                    needToMove2 = false;
                }
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
                else
                {
                    needToMove1 = false;
                }

                if (yDiffBomb2 < 2f) //Vertical
                {
                    bombe2.transform.position += transform.up * distanceDeltaHV;
                }
                else
                {
                    needToMove2 = false;
                }
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
    void PushWave()
    {
        //Push Player/IA/Bombe
    }

    /**************************************************************
     * Function : Le boss prend un dégat quand touché par bombes  *
     **************************************************************/
    public void Damages()
    {
        hp--;
        //seconds = 0;

        if(hp == 1)
        {
            Rage();
            Debug.Log("Grr Grr je rentre en rage");
        }

        if(hp == 0){isDead();}
    }

    /*****************************
     * Function : Le boss meurt  *
     *****************************/
    void isDead()
    {
        //Play Death of Mister Cloclo
    }
}
