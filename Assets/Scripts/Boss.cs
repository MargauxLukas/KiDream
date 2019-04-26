using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    Animator animator;

    public GameObject   bomb;
    private Transform target;
    private Rigidbody2D   rb;

    public  float speed = 0.2f;
    public float  timer;

    public int    hp   = 3;
    public int phase   = 0;
    public int seconds = 0;

	void Start ()
    {
        animator = GetComponent<Animator>()                            ;
        target   = GameObject.FindGameObjectWithTag("Player").transform;
        rb       = GetComponent<Rigidbody2D>()                         ;
	}
	
	void Update ()
    { 
        if(Time.time > timer + 1)
        {
            timer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }

        if(seconds == 1) // Fight commence à 1 seconde pour pas que le boss bouge direct apres la cinematique
        {
            phase = 1;
        }
        if(seconds == 6)
        {
            phase = 2;
        }
        if(seconds == 8)
        {
            seconds = 0;
        }
        /*if(numberOfPhase2 == 2)  //Lorsqu'il a fait deux dois la phase 2, il saute.
        {
            Jump();
        }*/

        if(phase == 1)       {       Move(); }
        else if (phase == 2) { LaunchBomb(); }
	}

    void LookAt()
    {
        float xDifference = Mathf.Abs(transform.position.x - target.position.x);
        float yDifference = Mathf.Abs(transform.position.y - target.position.y);

        if (xDifference > yDifference)
        {
            animator.SetFloat("moveY", 0f);
            if (target.position.x > transform.position.x) { animator.SetFloat("moveX",  1f);}
            if (target.position.x < transform.position.x) { animator.SetFloat("moveX", -1f);}

        }
        else if (xDifference < yDifference)
        {
            animator.SetFloat("moveX", 0f);
            if (target.position.y > transform.position.y) { animator.SetFloat("moveY",  1f);}
            if (target.position.y < transform.position.y) { animator.SetFloat("moveY", -1f);}
        }
    }

    void Sleep()
    {
        //Pas sur encore si c'est fait en cinematique
    }

    void Move()
    {
        animator.SetBool("isMoving", true);
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

    void LaunchBomb()
    {
        Debug.Log("Je lance des bombes ! ");
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
