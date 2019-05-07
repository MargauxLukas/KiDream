using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerByName : MonoBehaviour
{
    public string triggerer;

    public GameObject connectedGO;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains(triggerer))
        {
            connectedGO.SetActive(false);
        }
    }

}
