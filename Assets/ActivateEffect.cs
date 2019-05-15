using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEffect : MonoBehaviour
{
    ReactionToWave parentBehaviour;
    CircleCollider2D colRadius;
    ParticleSystem corruptedActivatePS;

    public ReactionToWave behaviour;
    public Rigidbody2D rb;

	// Start
	void Start ()
    {
        colRadius = this.GetComponent<CircleCollider2D>();
        parentBehaviour = this.GetComponentInParent<ReactionToWave>();
        corruptedActivatePS = this.GetComponent<ParticleSystem>();
        colRadius.radius = parentBehaviour.corruptedActivateRadius;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ActionObject") //Tous les objets ayant ce tag doivent avoir un BoxCollider2D (Normal), un RigidBody2D (Dynamic + GravityScale à 0) et le script ReactionToWave.
        {

            if (collision.GetComponent<ReactionToWave>() != null)
            {
                behaviour = collision.GetComponent<ReactionToWave>();
            }
            else if (collision.transform.parent.GetComponent<ReactionToWave>() != null)
            {
                behaviour = collision.GetComponentInParent<ReactionToWave>();
            }
            else
            {
                return;
            }

            if (behaviour.canBeActivated == true)
            {
                foreach(GameObject go in behaviour.whoCanShootMe)
                {
                    if(go == this.gameObject || go == this.transform.parent.gameObject)
                    {
                        behaviour.Activate();
                        behaviour.isActivated = true;
                    }
                }
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
