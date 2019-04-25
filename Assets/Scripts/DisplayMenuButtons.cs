using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMenuButtons : MonoBehaviour
{

    public Animator myMenuAnimator;

    void Start()
    {

        myMenuAnimator.SetTrigger("TriggerMenu");

    }

}
