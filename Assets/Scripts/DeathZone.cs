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
            collision.GetComponent<CharacterController>().isDead();
            StartCoroutine(Falling(collision.gameObject));
        }
        else if (collision.name.Contains("Chick"))
        {
            Debug.Log("chick est tombé");
            collision.GetComponent<ChickBehaviour>().Dead();
            StartCoroutine(Falling(collision.gameObject));
            Destroy(collision.gameObject);
            Destroy(collision.gameObject, 2f);
        }
        else if (collision.name.Contains("Grelotin"))
        {
            Debug.Log("Grelotin est tombé");
            collision.GetComponent<GrelotinBehaviour>().isDead();
            StartCoroutine(Falling(collision.gameObject));
            Destroy(collision.gameObject, 2f);
        }
        else if (collision.name.Contains("Pillar"))
        {
            Debug.Log("Pillier est tombé");
            StartCoroutine(Falling(collision.gameObject));
            Destroy(collision.gameObject, 2f);
        }
        else if (collision.name.Contains("Bomb"))
        {
            Debug.Log("Bombe est tombé");
            StartCoroutine(Falling(collision.gameObject));
            Destroy(collision.gameObject, 2f);
        }
    }

    IEnumerator Falling(GameObject gameObject)
    {
        if (gameObject != null)
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log("Je rentre");
                gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0);
                gameObject.transform.Rotate(0f, 0f, 20f);
                yield return new WaitForSeconds(0.100f);
            }
        }
    }
}
