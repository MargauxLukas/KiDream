using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    GameObject[] stairsTab;
    Animator anim;
    public GameObject stairsEntryCollider;
    public bool hasCollide = false;

    void Start ()
    {
        stairsTab = FindObjectsOfType<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && !hasCollide)
        {
            foreach (GameObject gameobject in stairsTab)
            {
                if (gameobject.name.Contains("Escalier"))
                {
                    anim = gameobject.GetComponent<Animator>();
                    anim.SetBool("isOpen", true);
                    StartCoroutine(Fade(gameobject));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasCollide = true;
    }

    IEnumerator Fade(GameObject gameobject)
    {
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7f);
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.8f);
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.9f);
        yield return new WaitForSeconds(0.1f);
        gameobject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(1.7f);
        Destroy(stairsEntryCollider);
    }
}