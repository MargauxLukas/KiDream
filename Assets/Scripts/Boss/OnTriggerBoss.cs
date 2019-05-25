using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerBoss : MonoBehaviour
{
    private GameObject boss;

    private void Start()
    {
        boss = GameObject.Find("Boss");
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision);
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
