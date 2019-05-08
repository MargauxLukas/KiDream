using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerBoss : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController>().damage();
        }
        if(collision.tag == "UIDetectionTag")
        {
            boss.GetComponent<Boss>().PushWave();
        }
    }
}
