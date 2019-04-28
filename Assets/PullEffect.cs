using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullEffect : MonoBehaviour
{
    //CircleCollider2D bColRadius;
    public Rigidbody2D rb;

    ReactionToWave behaviour;

    [Header("Forces horizontales et verticales")]
    [Range(0, 1000), SerializeField]
    private float forceX;
    [Range(0, 1000), SerializeField]
    private float forceY;
    public bool yEqualX = false;


    [Header("Attraction complémentaire")]
    [Range(0, 1000), SerializeField]
    private float strongWaveBonus;
    [SerializeField]
    private float timeBeforeNewStrongWave;
    [SerializeField]
    private float strongWaveDuration;



    //private float radiusValue;


    void Start()
    {
        InvokeRepeating("AttractionBonus", 0, timeBeforeNewStrongWave + strongWaveDuration);

        //bColRadius = this.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        //bColRadius.radius = radiusValue;
        if (yEqualX == true)
        {
            forceY = forceX;
        }
    }

    private void AttractionBonus()
    {
        StartCoroutine("StrongWaves");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2((this.transform.position.x - collision.transform.position.x) * forceX, (this.transform.position.y - collision.transform.position.y) * forceY));
        }
        else if (collision.tag == "ActionObject") //Tous les objets ayant ce tag doivent avoir un BoxCollider2D (Normal), un RigidBody2D (Dynamic + GravityScale à 0) et le script ReactionToWave.
        {
            behaviour = collision.GetComponent<ReactionToWave>();

            if (behaviour.canBePulled == true)
            {
                rb = collision.gameObject.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2((this.transform.position.x - collision.transform.position.x) * forceX * behaviour.HorizontalPull, (this.transform.position.y - collision.transform.position.y) * forceY * behaviour.VerticalPull));
            }
        }
    }

    IEnumerator StrongWaves()
    {
        forceX = forceX + strongWaveBonus;
        forceY = forceY + strongWaveBonus;
        yield return new WaitForSeconds(strongWaveDuration);
        forceX = forceX - strongWaveBonus;
        forceY = forceY - strongWaveBonus;
    }

}
