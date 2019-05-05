using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnnemyDetection : MonoBehaviour
{
    public AIDestinationSetter myTarget;
    public BAITAbility baitAbility;
    Transform baitTransform;

    // Use this for initialization
    void Start ()
    {
        baitTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        baitAbility.myTarget = myTarget;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ennemy")
        {
            myTarget = collision.gameObject.GetComponent<AIDestinationSetter>();
            myTarget.target = baitTransform.transform;
        }
    }

}
