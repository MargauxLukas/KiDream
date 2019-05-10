using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingBombCollision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponentInParent<BombFalling>().Explode(collision);
    }
}
