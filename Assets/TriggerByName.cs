using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerByName : MonoBehaviour
{
    public string triggerer;

    public GameObject connectedGO;

    public TriggerBehaviour enterBehaviour;
    public TriggerBehaviour exitBehaviour;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("Pillar"))
        {
            if(collision.gameObject.GetComponentInParent<OnCollisionPillar>().isActivated == false)
            {
                return;
            }
        }

        if(collision.gameObject.name.Contains(triggerer))
        {
            switch(enterBehaviour)
            {
                case TriggerBehaviour._SetActiveFalse:
                    connectedGO.SetActive(false);
                    break;

                case TriggerBehaviour._SetActiveTrue:
                    connectedGO.SetActive(true);
                    break;

                case TriggerBehaviour._Portal:
                    connectedGO.GetComponent<Animator>().SetBool("isOpen", true);
                    connectedGO.GetComponent<BoxCollider2D>().enabled = false;
                    break;

                case TriggerBehaviour._Null:
                    break;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains(triggerer))
        {
            switch (exitBehaviour)
            {
                case TriggerBehaviour._SetActiveFalse:
                    connectedGO.SetActive(false);
                    break;

                case TriggerBehaviour._SetActiveTrue:
                    connectedGO.SetActive(true);
                    break;

                case TriggerBehaviour._Portal:
                    connectedGO.GetComponent<Animator>().SetBool("isOpen", false);
                    connectedGO.GetComponent<BoxCollider2D>().enabled = false;
                    break;
                case TriggerBehaviour._Null:
                    break;
            }
        }
    }
}
public enum TriggerBehaviour {_SetActiveFalse, _SetActiveTrue, _Portal, _Null}
