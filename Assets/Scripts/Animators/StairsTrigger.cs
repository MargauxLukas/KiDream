using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    public List<GameObject> stairsTabDream = new List<GameObject>();
    public List<GameObject> stairsTabNightmare = new List<GameObject>();
    Animator anim;
    public GameObject stairsEntryCollider;
    public bool hasCollide = false;

    [Range(0, 10f), SerializeField]
    private float fadingRate;
    [Range(0, 1f), SerializeField]
    private float destroyingRate;

    void Start ()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && hasCollide == false)
        {
            StartCoroutine(ColorChangerDream());
            StartCoroutine(ColorChangerNightmare());
            StartCoroutine(Fade());
            hasCollide = true;
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(destroyingRate);
        Destroy(stairsEntryCollider);
    }

    IEnumerator Wait(GameObject gameObjectParameter, int index)
    {
        SpriteRenderer objectColor = gameObjectParameter.GetComponent<SpriteRenderer>();
        for (int i = 0; i < 255; i++)
        {
            yield return new WaitForSecondsRealtime(fadingRate);
            objectColor.color = new Color32(255, 255, 255, (byte)(i));

            if(i == 254)
            {
                index++;
            }
        }
    }

    /*IEnumerator ColorChangerOLD()
    {
        Debug.Log("Coroutine Started");

        int counter = 0;
        foreach (GameObject go in stairsTab)
        {
            Debug.Log("New Stair n°"+counter);
            if (stairsTab[counter] == go)
            {
                SpriteRenderer objectColor = go.GetComponent<SpriteRenderer>();
                for (int i = 0; i < 255; i++)
                {
                    yield return new WaitForSeconds(fadingRate);
                    objectColor.color = new Color32(255, 255, 255, (byte)(i));

                    if (i == 254)
                    {
                        counter++;
                    }
                }
            }
        }
    }*/

    IEnumerator ColorChangerDream()
    {

        int counter = 0;
        foreach (GameObject go in stairsTabDream)
        {
            if (stairsTabDream[counter] == go)
            {
                SpriteRenderer objectColor = go.GetComponent<SpriteRenderer>();
                float alphaValue = 0;

                while (alphaValue < 255)
                {
                    objectColor.color = new Color32(255, 255, 255, (byte)(alphaValue));
                    alphaValue += 1 + fadingRate;
                    yield return null;
                }

                counter++;
            }
        }
    }

    IEnumerator ColorChangerNightmare()
    {

        int counter = 0;
        foreach (GameObject go in stairsTabNightmare)
        {
            if (stairsTabNightmare[counter] == go)
            {
                SpriteRenderer objectColor = go.GetComponent<SpriteRenderer>();
                float alphaValue = 0;

                while (alphaValue < 255)
                {
                    objectColor.color = new Color32(255, 255, 255, (byte)(alphaValue));
                    alphaValue += 1 + fadingRate;
                    yield return null;
                }

                counter++;
            }
        }
    }
}