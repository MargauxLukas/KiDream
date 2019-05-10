using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombCollision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponentInParent <Bomb>().Explode(collision);
    }
}
