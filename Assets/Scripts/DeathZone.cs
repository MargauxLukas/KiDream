using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        if (collision.name == "Player")
        {
            Debug.Log("Player est tombé");
            //Animation de mort
            //Respawn
        }
        else if (collision.tag.Contains("Chick"))
        {
            Debug.Log("chick est tombé");
            //Animation de mort
        }
        else if (collision.name.Contains("Pillar"))
        {
            Debug.Log("Pillier est tombé");
        }
    }
}
