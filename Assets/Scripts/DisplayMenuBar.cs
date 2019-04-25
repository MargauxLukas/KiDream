using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DisplayMenuBar : MonoBehaviour
{

    public GameObject options;
    public bool canHide = false;


    void Start()
    {
        options.SetActive(false);
    }

    public void ShowMenu()
    {
        if (canHide == false)
        {
            options.SetActive(true);
            canHide = true;
        }

        else if (canHide == true)
        {
            options.SetActive(false);
            canHide = false;
        }

    }



}
