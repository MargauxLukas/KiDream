using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    public string triggerToBeDestroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Debug.Log(collision.gameObject.name);
        //Debug.Log(collision.transform.parent.gameObject.name);

        if(collision.gameObject.name.Contains(triggerToBeDestroyed) || collision.transform.parent.gameObject.name.Contains(triggerToBeDestroyed))
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Nothing Has Been Destroyed");
        }
    }
}
