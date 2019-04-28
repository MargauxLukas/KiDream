using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEffect : MonoBehaviour
{

    ReactionToWave behaviour;
    Rigidbody2D rb;

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
            behaviour = collision.GetComponent<ReactionToWave>();

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
            behaviour = collision.GetComponent<ReactionToWave>();

            if (behaviour.canBeActivated == true)
            {
                behaviour.isActivated = false;
            }
        }
    }
}
