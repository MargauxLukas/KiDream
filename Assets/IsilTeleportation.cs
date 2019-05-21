using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsilTeleportation : MonoBehaviour
{
    private Animator animator;

    public List<Transform> positionList = new List<Transform>();
    public List<int> dialogueExecutionStatutNumber = new List<int>();
    public List<GameObject> triggerList = new List<GameObject>();

    private int n=0;

    private void Start()
    {
        foreach(GameObject trigger in triggerList)
        {
            trigger.GetComponent<BoxCollider2D>().enabled = false;
        }

        animator = GetComponent<Animator>();
    }

    void Update ()
    {
        if(DialogueManager.dialogueExecutionStatut == dialogueExecutionStatutNumber[n])
        {
            StartCoroutine(TeleportationC(n));
            n++;
            triggerList[n - 1].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    IEnumerator TeleportationC(int n1)
    {
        triggerList[n1].GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("isMuted", true);
        yield return new WaitForSeconds(1.2f);
        Debug.Log(gameObject.transform.position + " to " + positionList[n1].position);
        gameObject.transform.position = positionList[n1].position;
    }
}
