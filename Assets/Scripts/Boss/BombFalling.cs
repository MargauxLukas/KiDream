using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFalling : MonoBehaviour
{
    private Animator   animator;

    private GameObject boss    ;
    private GameObject player  ;
    private WaveManager waveManager;

    private Collider2D collider;
    public GameObject explosionArea;

    public Vector2 target;

    private bool isDream;

    [Header("Timer")]
    [SerializeField]public float explosionTime;
    [SerializeField] float timer        ;

    [Header("Bombe Object")]
    public GameObject shadowBomb;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator  >();
        boss     = GameObject.Find("Boss"  );
        player   = GameObject.Find("Player");
        waveManager = FindObjectOfType<WaveManager>();
        GetComponent<ReactionToWave>().whoCanShootMe.Add(player);
        GetComponent<ReactionToWave>().whoCanShootMe.Add(boss);
        GetComponent<ReactionToWave>().waveManager = waveManager;
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= explosionTime - 4.380f){animator.SetBool("isTimer", true);}
        if (timer >= explosionTime - 0.583f){ explosionArea.SetActive(true)   ;}
        if (timer >= explosionTime         ){Explode()                        ;}

        isDream = player.GetComponent<CharacterController>().isDream;

        if (isDream) { animator.SetBool("isDream", true ); }
        else         { animator.SetBool("isDream", false); }

        if (transform.position.y > target.y)
        {
            animator.SetBool("isFalling", true);
            collider.isTrigger = true;
            transform.Translate(-transform.up * 0.03f);
        }
        if (transform.position.y < target.y)
        {
            animator.SetBool("isFalling", false);
            collider.isTrigger = false;
            transform.Translate(0,0,0);
            Destroy(shadowBomb);
        }
    }

    void Explode(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("isExplode", true);
            collision.gameObject.GetComponent<CharacterController>().damage();
            Destroy(gameObject, 0.6f    );
        }
        else if (collision.gameObject.name == "WallCollider")
        {
            animator.SetBool("isExplode", true);
            Destroy(gameObject, 0.6f);
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transform.position.x > 3.40f || transform.position.x < -3.40f || transform.position.y > 2.8f || transform.position.y < -1.5f)
        {
            Explode();
        }
    }
}
