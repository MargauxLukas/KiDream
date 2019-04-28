using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator animator;

    public  GameObject         bomb;
    public  GameObject        ombre;
    public  GameObject bombLauncher;
    public  GameObject  ombreObject;

    private GameObject bombe1;
    private GameObject bombe2;
    private GameObject bombe3;

    private Transform target;
    private Rigidbody2D   rb;

    public float speed = 0.2f;
    public float timer       ;

    public bool isRage = false;
    public bool bossFallDown = false;
    public bool isStartingPhase = true;

    public int        hp = 3;
    public int   seconds = 0;
    public int lookingAt = 0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up

    private int       direction = 0;
    private float   distanceDeltaHV;
    private float distanceDeltaDiag;

    private bool needToMove1 = false;
    private bool needToMove2 = false;

    void Start ()
    {
        distanceDeltaHV   = Time.deltaTime *   1f;
        distanceDeltaDiag = Time.deltaTime * 0.5f;
        animator = GetComponent<Animator>();
        target   = GameObject.FindGameObjectWithTag("Player").transform;
        rb       = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        if (needToMove1==true || needToMove2==true)
        {
            MovingBomb();
        }
        else
        {

        }

        if (Time.time > timer + 1)
        {
            timer = Time.time;
            seconds++;
        }
        if (hp == 3)
        {
            if (seconds >= 1) // Fight commence à 1 seconde pour pas que le boss bouge direct apres la cinematique
            {
                animator.SetBool("isMoving", true);
                Move();
            }
            if (seconds == 6)
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isLaunching", true);
                direction = ThrowBomb(lookingAt);
                seconds = -5;
                animator.SetBool("isLaunching", false);
            }
        }

        if(hp == 2)
        {
            if (isStartingPhase)
            {
                if (!bossFallDown)
                {
                    animator.SetBool("isJumping", true);
                    Jump();
                }

                if (bossFallDown)
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
                    animator.SetBool("isMoving", false);
                    animator.SetBool("isLaunching", true);
                    direction = ThrowBomb(lookingAt);
                    seconds = -5;
                    animator.SetBool("isLaunching", false);
                }
            }
        }
    }

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
                lookingAt = 1;
            }
            if (target.position.x < transform.position.x)
            {
                animator.SetFloat("moveX", -1f);
                lookingAt = 3;
            }
        }
        else if (xDifference < yDifference)
        {
            animator.SetFloat("moveX", 0f);
            if (target.position.y > transform.position.y)
            {
                animator.SetFloat("moveY",  1f);
                lookingAt = 4;
            }
            if (target.position.y < transform.position.y)
            {
                animator.SetFloat("moveY", -1f);
                lookingAt = 2;
            }
        }
    }

    void Move()
    {
        LookAt();
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void Jump()
    {
        StartCoroutine(WaitJump());
        //Le boss saute et atteri détruisant les piliers autours.
    }

    IEnumerator WaitJump()
    {
        Debug.Log("Je passe par là ? ");
        yield return new WaitForSeconds(0.9f);
        gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 4.5f), 0.1f);
        yield return new WaitForSeconds(0.9f);
        Shadow();
    }

    void Shadow()
    {
        if (ombreObject == null)
        {
            ombreObject = Instantiate(ombre, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3.50f, 0), Quaternion.identity);
        }
        Debug.Log("Je passe là");
        ombreObject.transform.position = Vector2.MoveTowards(ombreObject.transform.position, target.position, 1f * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(ombreObject.transform.position.x, transform.position.y), 1f * Time.deltaTime);
        StartCoroutine(WaitShadow());
    }

    IEnumerator WaitShadow()
    {
        yield return new WaitForSeconds(2f);
        bossFallDown = true;
    }

    void BossLanding()
    {
        gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, ombreObject.transform.position.y), 0.1f);
        StartCoroutine(WaitLanding());
    }

    IEnumerator WaitLanding()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isLanding", true);
    }

    void Rage()
    {
        //Détruit tout les murs autours de la salle
        isRage = true;
    }

    void PushPlayer()
    {
        //Push Player if is very trop pret
    }

    int ThrowBomb(int lookingAt)
    {
        Debug.Log("Je lance des bombes vers " + lookingAt);

        switch(lookingAt)
        {
            case 1:
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                needToMove1 = true;
                needToMove2 = true;
                return 1;
            case 2:
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                needToMove1 = true;
                needToMove2 = true;
                return 2;
            case 3:
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                needToMove1 = true;
                needToMove2 = true;
                return 3;
            case 4:
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                needToMove1 = true;
                needToMove2 = true;
                return 4;
            default:
                return 0;
        }
    }

    void MovingBomb()
    {
        if (direction != 0 && bombe1 != null && bombe2 != null && bombe3 != null)
        {
            if (direction == 1)
            {
                Debug.Log("Est");
                float xDiffBomb1 = Mathf.Abs(transform.position.x - bombe1.transform.position.x);
                float xDiffBomb2 = Mathf.Abs(transform.position.x - bombe2.transform.position.x);

                if (xDiffBomb1 < 0.60f)
                {
                    bombe1.transform.Translate(transform.up * distanceDeltaDiag + transform.right * distanceDeltaDiag);
                    bombe3.transform.Translate(-transform.up * distanceDeltaDiag + transform.right * distanceDeltaDiag);
                }
                else
                {
                    needToMove1 = false;
                }

                if (xDiffBomb2 < 1.5f)
                {
                    bombe2.transform.position += transform.right * distanceDeltaHV;
                }
                else
                {
                    needToMove2 = false;
                }
            }
            else if (direction == 2)
            {
                Debug.Log("Sud");

                float yDiffBomb1 = Mathf.Abs(transform.position.y - bombe1.transform.position.y);
                float yDiffBomb2 = Mathf.Abs(transform.position.y - bombe2.transform.position.y);

                if (yDiffBomb1 < 1f)
                {
                    bombe1.transform.Translate(-transform.up * distanceDeltaDiag + transform.right * distanceDeltaDiag);
                    bombe3.transform.Translate(-transform.up * distanceDeltaDiag + -transform.right * distanceDeltaDiag);
                }
                else
                {
                    needToMove1 = false;
                }

                if (yDiffBomb2 < 2f)
                {
                    bombe2.transform.position += -transform.up * distanceDeltaHV;
                }
                else
                {
                    needToMove2 = false;
                }
            }
            else if (direction == 3)
            {
                Debug.Log("Ouest");

                float xDiffBomb1 = Mathf.Abs(transform.position.x - bombe1.transform.position.x);
                float xDiffBomb2 = Mathf.Abs(transform.position.x - bombe2.transform.position.x);

                if (xDiffBomb1 < 1f)
                {
                    bombe1.transform.Translate(transform.up * distanceDeltaDiag + -transform.right * distanceDeltaDiag);
                    bombe3.transform.Translate(-transform.up * distanceDeltaDiag + -transform.right * distanceDeltaDiag);
                }
                else
                {
                    needToMove1 = false;
                }

                if (xDiffBomb2 < 2f)
                {
                    bombe2.transform.position += -transform.right * distanceDeltaHV;
                }
                else
                {
                    needToMove2 = false;
                }
            }
            else if (direction == 4)
            {
                Debug.Log("Nord");

                float yDiffBomb1 = Mathf.Abs(transform.position.y - bombe1.transform.position.y);
                float yDiffBomb2 = Mathf.Abs(transform.position.y - bombe2.transform.position.y);

                if (yDiffBomb1 < 1f)
                {
                    bombe1.transform.Translate(transform.up * distanceDeltaDiag + transform.right * distanceDeltaDiag);
                    bombe3.transform.Translate(transform.up * distanceDeltaDiag + -transform.right * distanceDeltaDiag);
                }
                else
                {
                    needToMove1 = false;
                }

                if (yDiffBomb2 < 2f)
                {
                    bombe2.transform.position += transform.up * distanceDeltaHV;
                }
                else
                {
                    needToMove2 = false;
                }
            }
        }
    }

    void MobInvoking()
    {
        //Invoque les petits mobs clochettes "Grelottins".
    }

    void PushWave()
    {
        //Push Player/IA/Bombe
    }

    public void Damages()
    {
        hp--;
        seconds = 0;

        if(hp == 1)
        {
            Rage();
            Debug.Log("Grr Grr je rentre en rage");
        }

        if(hp == 0)
        {
            isDead();
        }
    }

    void isDead()
    {
        //Play Death of Mister Cloclo
    }
}
