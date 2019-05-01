using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : MonoBehaviour
{
    CircleCollider2D colRadius;
    ParticleSystem corruptedPushPS;

    public Rigidbody2D rb;

    [Header("Forces horizontales et verticales")]
    [Range(0,1000), SerializeField]
    private float forceX;
    [Range(0, 100), SerializeField]
    private float forceY;
    public bool yEqualX = false;

    //private float radiusValue;


    void Start()
    {
        colRadius = this.GetComponent<CircleCollider2D>();
        corruptedPushPS = this.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //bColRadius.radius = radiusValue;

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

            Vector2 rotationVector = new Vector2(collision.transform.position.x - xInitial, collision.transform.position.y - yInitial);
            float angleValue = Mathf.Atan2(rotationVector.normalized.y, rotationVector.normalized.x) * Mathf.Rad2Deg;

            /*float x = xInitial + bColRadius.radius * Mathf.Cos(angleValue);
            float y = yInitial + bColRadius.radius * Mathf.Sin(angleValue);*/

            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce((collision.transform.position - transform.position)*forceX);
        }
        else if(collision.tag == "ActionObject") //Tous les objets ayant ce tag doivent avoir un BoxCollider2D (Normal), un RigidBody2D (Dynamic + GravityScale à 0) et le script ReactionToWave.
        {
            ReactionToWave behaviour = collision.GetComponent<ReactionToWave>();

            if(behaviour.canBePushed == true)
            {
                colRadius.radius = behaviour.cPushRadius;
                rb = collision.gameObject.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(-(this.transform.position.x - collision.transform.position.x) * forceX, -(this.transform.position.y - collision.transform.position.y) * forceY));
            }
        }
                    
    }


}
