using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEffect : MonoBehaviour
{

    public ReactionToWave behaviour;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ActionObject") //Tous les objets ayant ce tag doivent avoir un BoxCollider2D (Normal), un RigidBody2D (Dynamic + GravityScale à 0) et le script ReactionToWave.
        {

            if (collision.GetComponent<ReactionToWave>() == null)
            {
                return;
            }
            else if(collision.GetComponent<ReactionToWave>() != null)
            {
                behaviour = collision.GetComponent<ReactionToWave>();
            }

            if (collision.GetComponentInParent<ReactionToWave>() == null)
            {
                return;
            }
            else if (collision.GetComponentInParent<ReactionToWave>() != null)
            {
                behaviour = collision.GetComponentInParent<ReactionToWave>();
            }


            if (behaviour.canBeActivated == true)
            {

                behaviour.isActivated = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ActionObject") //Tous les objets ayant ce tag doivent avoir un BoxCollider2D (Normal), un RigidBody2D (Dynamic + GravityScale à 0) et le script ReactionToWave.
        {
            if (collision.GetComponent<ReactionToWave>() == null)
            {
                return;
            }

            behaviour = collision.GetComponent<ReactionToWave>();

            if (behaviour.canBeActivated == true)
            {
                behaviour.isActivated = false;
            }
        }
    }
}
