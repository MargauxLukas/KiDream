using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxSpawnBackground : MonoBehaviour
{
    public GameObject paralaxBackground;
    public GameObject paralaxStar;
    public GameObject paralaxDust;
    public GameObject paralaxNebula;
    public GameObject backgroundCamera;
    public int counter = 0;

    private int numberBg = 0;
    private int random = 0;
    private Vector3 randomPosition;
    private bool isBackground = false;

    Transform transCamera;

    private void Awake()
    {
        transCamera = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        

        //Spawn Background

        for (int i = 0; i < 24; i= i + 5)
        {
            if (counter == 0 || counter >= 4000)
            {
                randomPosition.x = transCamera.position.x;
                randomPosition.y = transCamera.position.y + i;
                randomPosition.z = 0;
                randomPosition.y = randomPosition.y + i;

                GameObject instance = (GameObject)GameObject.Instantiate(paralaxBackground, randomPosition, paralaxBackground.transform.rotation);
                instance.transform.parent = GameObject.FindGameObjectWithTag("ParalaxBackground").transform;
                counter = 0;
            }
        }
        //Spawn Nebula
        if (counter == 400 || counter == 2400)
        {
            random = Mathf.RoundToInt(Random.Range(1, 1.99f));
            randomPosition.x = transCamera.position.x - 5.9f;
            randomPosition.y = Random.Range(transCamera.position.y - 4.56f, transCamera.position.y + 4.44f);
            randomPosition.z = 0;

            switch (random)
            {
                case(1):
                    GameObject instance1 = (GameObject)GameObject.Instantiate(paralaxNebula, randomPosition, paralaxNebula.transform.rotation);
                    instance1.transform.parent = GameObject.FindGameObjectWithTag("ParalaxBackground").transform;
                    break;

            }
        }

        //Spawn Dust
        if (counter == 500 || counter == 1500 || counter == 2500 || counter == 3500)
        {
            random = Mathf.RoundToInt(Random.Range(1, 1.99f));
            randomPosition.x = transCamera.position.x - 3.8f;
            randomPosition.y = Random.Range(transCamera.position.y - 2.16f, transCamera.position.y + 1.94f);
            randomPosition.z = 0;

            switch (random)
            {
                case (1):
                    GameObject instance1 = (GameObject)GameObject.Instantiate(paralaxDust, randomPosition, paralaxDust.transform.rotation);
                    instance1.transform.parent = GameObject.FindGameObjectWithTag("ParalaxBackground").transform;
                    break;

            }
        }

        //Spawn Stars
        if (counter == 1 || counter == 750 || counter == 2500 || counter == 3000 || counter == 3750)
        {
            random = Mathf.RoundToInt(Random.Range(1, 1.99f));
            randomPosition.x = transCamera.position.x - 2.5f;
            randomPosition.y = Random.Range(transCamera.position.y - 1.16f, transCamera.position.y + 1.04f);
            randomPosition.z = 0;

            switch (random)
            {
                case (1):
                    GameObject instance1 = (GameObject)GameObject.Instantiate(paralaxStar, randomPosition, paralaxStar.transform.rotation);
                    instance1.transform.parent = GameObject.FindGameObjectWithTag("ParalaxBackground").transform;
                    break;

            }
        }
        counter += 1;
	}
}
