using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerByName : MonoBehaviour
{
    public string triggerer;

    public GameObject connectedGO;

    public TriggerBehaviour behaviour;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains(triggerer))
        {
            switch(behaviour)
            {
                case TriggerBehaviour._SetActiveFalse:
                    connectedGO.SetActive(false);
                    break;
            }
        }
    }

}
public enum TriggerBehaviour {_SetActiveFalse}