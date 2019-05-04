using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFalling : MonoBehaviour
{
    public Vector2  target;
    public float positionX;
    public float positionY;

    [SerializeField] float explosionTime;
    [SerializeField] float timer;

    private bool isDream ;
    Animator   animator  ;

    GameObject boss  ;
    GameObject player;
    public GameObject shadowBomb;
    public Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator  >();
        boss     = GameObject.Find("Boss"  );
        player   = GameObject.Find("Player");
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= explosionTime - 4.380f)
        {
            animator.SetBool("isTimer", true);
        }
        if (timer >= explosionTime)
        {
            Explode();
        }

        isDream = player.GetComponent<CharacterController>().isDream;
        if (isDream) { animator.SetBool("isDream", true ); }
        else         { animator.SetBool("isDream", false); }
        //transform.position = Vector2.MoveTowards(new Vector2(positionX, 4f),target, 1 * Time.deltaTime);

        if (transform.position.y > target.y)
        {
            animator.SetBool("isFalling", true);
            transform.Translate(-transform.up * 0.03f);
        }
        if (transform.position.y < target.y)
        {
            animator.SetBool("isFalling", false);
            transform.Translate(0,0,0);
            Destroy(shadowBomb);
        }

        if (transform.position.y > 2.05f || transform.position.y < -0.8f)
        {
            collider.isTrigger = true;
        }
        else
        {
            collider.isTrigger = false;
        }
    }

    void Explode(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("isExplode", true);
            Destroy(collision.gameObject);
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
}
