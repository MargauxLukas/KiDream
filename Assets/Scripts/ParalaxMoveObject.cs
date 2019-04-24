using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxMoveObject : MonoBehaviour
{
    [SerializeField] float speed = 1f;

	void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
}
