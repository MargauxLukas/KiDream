using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController>().damage();
        }
        if(collision.tag == "ActionObject")
        {
            GetComponentInParent<Boss>().PushWave();
        }
    }
}
