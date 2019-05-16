using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeCollider : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            Debug.Log("Player detected");
            player.GetComponent<CharacterController>().damage();
        }
    }
}
