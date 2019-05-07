using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    public string triggerToDestroy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == triggerToDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
