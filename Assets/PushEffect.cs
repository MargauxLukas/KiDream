using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : MonoBehaviour
{
    CircleCollider2D colRadius;
    ParticleSystem corruptedPushPS;
    ReactionToWave parentBehaviour;

    public Rigidbody2D rb;

    [Header("Forces horizontales et verticales")]
    [Range(0,1000), SerializeField]
    private float forceX;
    [Range(0, 100), SerializeField]
    private float forceY;
    public bool yEqualX = false;


    void Start()
    {
        colRadius = this.GetComponent<CircleCollider2D>();
        corruptedPushPS = this.GetComponent<ParticleSystem>();
        parentBehaviour = this.GetComponentInParent<ReactionToWave>();
        colRadius.radius = parentBehaviour.corruptedPushRadius;
    }


    void Update()
    {
        if (yEqualX == true)
        {
            forceY = forceX;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
                rb.AddForce(new Vector2(-(this.transform.position.x - collision.transform.position.x) * behaviour.horizontalPushForce * forceX, -(this.transform.position.y - collision.transform.position.y) * behaviour.verticalPushForce * forceY), ForceMode2D.Impulse);
            }
        }
                    
    }


}
