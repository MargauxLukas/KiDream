﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator animator;

    public  GameObject   bomb;
    private GameObject bombe1;
    private GameObject bombe2;
    private GameObject bombe3;
    public GameObject bombLauncher;
    private Transform target;
    private Rigidbody2D   rb;

    public  float speed = 0.2f;
    public float  timer;

    public int    hp   = 3;
    public int phase   = 0; // 0 = Nothing, 1 = Move, 2 = Throw Bomb
    public int seconds = 0;
    public int lookingAt = 0; // 1 = Droite, 2 = Down, 3 = Left, 4 = Up
    private int direction = 0;
    private float distanceDeltaHV;
    private float distanceDeltaDiag;

    void Start ()
    {
        distanceDeltaHV = Time.deltaTime * 1f;
        distanceDeltaDiag = Time.deltaTime * 0.5f;
        animator = GetComponent<Animator>()                            ;
        target   = GameObject.FindGameObjectWithTag("Player").transform;
        rb       = GetComponent<Rigidbody2D>()                         ;
	}
	
	void Update ()
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
                    bombe1.transform.Translate(0,0,0);
                    bombe3.transform.Translate(0,0,0);
                }

                if (xDiffBomb2 < 1.5f)
                {
                    bombe2.transform.position += transform.right * distanceDeltaHV;
                }
                else
                {
                    bombe2.transform.Translate(0, 0, 0);
                }
            }
            else if (direction == 2)
            {
                Debug.Log("Sud");
            
                float yDiffBomb1 = Mathf.Abs(transform.position.y - bombe1.transform.position.y);
                float yDiffBomb2 = Mathf.Abs(transform.position.y - bombe2.transform.position.y);

                Debug.Log("1 " + yDiffBomb1);
                Debug.Log("2 " + yDiffBomb2);

                if (yDiffBomb1 < 1f)
                {
                    bombe1.transform.Translate(-transform.up * distanceDeltaDiag + transform.right * distanceDeltaDiag);
                    bombe3.transform.Translate(-transform.up * distanceDeltaDiag + -transform.right * distanceDeltaDiag);
                }
                else
                {
                    bombe1.transform.Translate(0, 0, 0);
                    bombe3.transform.Translate(0, 0, 0);
                }

                if (yDiffBomb2 < 2f)
                {
                    bombe2.transform.position += -transform.up * distanceDeltaHV;
                }
                else
                {
                    bombe2.transform.Translate(0, 0, 0);
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
                    bombe1.transform.Translate(0, 0, 0);
                    bombe3.transform.Translate(0, 0, 0);
                }

                if (xDiffBomb2 < 2f)
                {
                    bombe2.transform.position += -transform.right * distanceDeltaHV;
                }
                else
                {
                    bombe2.transform.Translate(0, 0, 0);
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
                    bombe1.transform.Translate(0, 0, 0);
                    bombe3.transform.Translate(0, 0, 0);
                }

                if (yDiffBomb2 < 2f)
                {
                    bombe2.transform.position += transform.up * distanceDeltaHV;
                }
                else
                {
                    bombe2.transform.Translate(0, 0, 0);
                }
            }
        }

        if(Time.time > timer + 1)
        {
            timer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }
    
        if (seconds < 0)
        {
            phase = 0;
        }
        if(seconds == 1) // Fight commence à 1 seconde pour pas que le boss bouge direct apres la cinematique
        {
            animator.SetBool("isMoving", true);
            phase = 1;
        }
        if(seconds == 6)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isLaunching", true);
            phase = 2;
        }
        /*if(numberOfPhase2 == 2)  //Lorsqu'il a fait deux dois la phase 2, il saute.
        {
            Jump();
        }*/

        if      (phase == 1){ Move(); }
        else if (phase == 2)
        {
            direction = ThrowBomb(lookingAt);
            seconds = -5;
            animator.SetBool("isLaunching", false);
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

    void Sleep()
    {
        //Pas sur encore si c'est fait en cinematique
    }

    void Move()
    {
        LookAt();
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void Jump()
    {
        //Le boss saute et atteri détruisant les piliers autours.
    }

    void Rage()
    {
        //Détruit tout les murs autours de la salle
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
                return 1;
            case 2:
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                return 2;
            case 3:
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                return 3;
            case 4:
                bombe1 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe2 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                bombe3 = Instantiate(bomb, new Vector3(bombLauncher.transform.position.x, bombLauncher.transform.position.y, bombLauncher.transform.position.z), Quaternion.identity);
                return 4;
            default:
                return 0;
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

    void Damages()
    {
        hp--;

        if(hp == 1)
        {
            Rage();
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
