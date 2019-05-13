using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : MonoBehaviour
{
    CircleCollider2D colRadius;
    ParticleSystem corruptedPushPS;
    ReactionToWave parentBehaviour;

    public bool affectPlayer;

    public Rigidbody2D rb;

    [Header("Forces horizontales et verticales")]
    [Range(0, 1000), SerializeField]
    public float forceY;
    [Range(0,1000), SerializeField]
    public float forceX;

    public bool xEqualY;

    public bool adaptRadius;


    void Start()
    {
        colRadius = this.GetComponent<CircleCollider2D>();
        corruptedPushPS = this.GetComponent<ParticleSystem>();
        parentBehaviour = this.GetComponentInParent<ReactionToWave>();
        if(adaptRadius == true)
        {
            colRadius.radius = parentBehaviour.corruptedPushRadius;
        }
    }


    void Update()
    {
        if (xEqualY == true)
        {
            forceX = forceY;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && affectPlayer == true)
        {
            float xInitial = this.transform.position.x;
            float yInitial = this.transform.position.y;

            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce((collision.transform.position - transform.position)*forceX);
        }
        else if(collision.tag == "ActionObject") //Tous les objets ayant ce tag doivent avoir un BoxCollider2D (Normal), un RigidBody2D (Dynamic + GravityScale à 0) et le script ReactionToWave.
        {
            ReactionToWave behaviour = collision.GetComponent<ReactionToWave>();

            if (behaviour.canBePushed == true)
            {
                rb = collision.gameObject.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(-(this.transform.position.x - collision.transform.position.x) * forceX, -(this.transform.position.y - collision.transform.position.y) * forceY), ForceMode2D.Impulse);
            }
        }
                    
    }


}
