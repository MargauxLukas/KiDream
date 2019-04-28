using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : MonoBehaviour
{
    CircleCollider2D bColRadius;
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
        bColRadius = this.GetComponent<CircleCollider2D>();
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

            //Debug.Log(angleValue);

            float x = xInitial + bColRadius.radius * Mathf.Cos(angleValue);
            float y = yInitial + bColRadius.radius * Mathf.Sin(angleValue);

            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce((collision.transform.position - transform.position)*forceX);
            Debug.Log(new Vector2(x, y));
        }
        else if(collision.tag == "ActionObject") //Tous les objets ayant ce tag doivent avoir un BoxCollider2D (Normal), un RigidBody2D (Dynamic + GravityScale à 0) et le script ReactionToWave.
        {
            ReactionToWave behaviour = collision.GetComponent<ReactionToWave>();

            if(behaviour.canBePushed == true)
            {
                Debug.Log("Yoooo");
                rb = collision.gameObject.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2((this.transform.position.x - collision.transform.position.x) * forceX, (this.transform.position.y - collision.transform.position.y) * forceY));
            }
        }
                    //où(x0, y0) sont les coord du centre, r est le rayon, et t l'angle.
    }


}
