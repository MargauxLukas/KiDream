using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScreen : MonoBehaviour
{

    public SpriteRenderer[] screens = new SpriteRenderer[2];

    private bool selectorInUse = false;

    SpriteRenderer sr;

    // Start
    void Start ()
    {
        screens[0] = this.transform.Find("ecrantitre_unity_dream").GetComponentInChildren<SpriteRenderer>();
        screens[1] = this.transform.Find("ecrantitre_unity_nightmare").GetComponentInChildren<SpriteRenderer>();

        sr = screens[(Random.Range(0, 2))];

        sr.sortingOrder = 5;
	}

    // Update
    void Update()
    {
        float selector = Input.GetAxisRaw("ChangeWorld");

        if (selector > 0 && selectorInUse == false)
        {
            if (sr.sortingOrder == 1)
            {
                sr.sortingOrder = 5;
            }
            else
            {
                sr.sortingOrder = 1;
            }

            selectorInUse = true;
        }
        else if(selector == 0)
        {
            selectorInUse = false;
        }

        float pass = Input.GetAxisRaw("ShootParticles");

        if(pass > 0)
        {
            SceneManager.LoadScene(0);
        }

    }
}
