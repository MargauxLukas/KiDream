using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator animator;

    [Header("Timer")]
    [SerializeField] float explosionTime;
    [SerializeField] float timer;

    private bool canHurtBoss = false;
    private bool isDream     =  true;

    CircleCollider2D  collider     ;
    public GameObject explosionArea;

    GameObject boss;
    GameObject player;
    WaveManager waveManager;

	void Start ()
    {
        animator = GetComponent<Animator>() ;
        boss     = GameObject.Find("Boss"  );
        player   = GameObject.Find("Player");
        waveManager = FindObjectOfType<WaveManager>();
        collider = gameObject.GetComponent<CircleCollider2D>();
        GetComponent<ReactionToWave>().whoCanShootMe.Add(player);
        GetComponent<ReactionToWave>().whoCanShootMe.Add(boss);
        GetComponent<ReactionToWave>().waveManager = waveManager;

    }
	
	void Update ()
    {
        isDream = player.GetComponent<CharacterController>().isDream;
        if (isDream) { animator.SetBool("isDream",  true);}
        else         { animator.SetBool("isDream", false);}

        timer += Time.deltaTime;
        if (timer >= explosionTime - 4.380f) {animator.SetBool("isTimer", true);}
        if (timer >= explosionTime - 0.583f) {explosionArea.SetActive(true)    ;}
        if (timer >= explosionTime         ) {Explode()                        ;}
	}

    void Explode(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("isExplode", true);
            collision.gameObject.GetComponent<CharacterController>().damage();
            Destroy(gameObject, 0.6f    );
        }
        else if (collision.gameObject.tag == "Boss" && canHurtBoss)
        {
            animator.SetBool("isExplode", true);
            boss.GetComponent<Boss>().Damages();
            Destroy(gameObject, 0.55f);
        }
        else if(collision.gameObject.name == "WallCollider")
        {
            animator.SetBool("isExplode", true);
            Destroy(gameObject, 0.55f);
        }
    }

    void Explode()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collider.isTrigger = false;

        if (transform.position.x > 3.40f || transform.position.x < -3.40f || transform.position.y > 2.8f || transform.position.y < -1.5f)
        {
            Explode();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        canHurtBoss = true;
    }
}
