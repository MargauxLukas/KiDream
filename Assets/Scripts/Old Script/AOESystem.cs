using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AOESystem : MonoBehaviour
{
    private GameObject[] aoeVList = new GameObject[10];
    private GameObject[] aoeHList = new GameObject[10];
    private GameObject[] aoeCircleList = new GameObject[10];

    // Use this for initialization
    void Awake ()
    {
        GameObject[] aoeList = GameObject.FindGameObjectsWithTag("DeathZone");
        int iV = 0;
        int iH = 0;
        int iCircle = 0;

        for(int i=0; i< aoeList.Length;i++)
        {
            if (aoeList[i].name.Contains("AoeV"))
            {
                iV = getTheNumber(aoeList[i].name);
                aoeVList[iV] = aoeList[i];
                aoeVList[iV].SetActive(false);
                iV++;
            }
            else if (aoeList[i].name.Contains("AoeH"))
            {
                iH = getTheNumber(aoeList[i].name);
                aoeHList[iH] = aoeList[i];
                aoeHList[iH].SetActive(false);
                iH++;
            }
            else if (aoeList[i].name.Contains("AoeCircle"))
            {
                iCircle = getTheNumber(aoeList[i].name);
                aoeCircleList[iCircle] = aoeList[i];
                aoeCircleList[iCircle].SetActive(false);
                iCircle++;
            }
        }
    }

    public void PlayAOEPuzzle()
    {
        int i = 5;
   
        //Aoe V
        StartCoroutine(WaitActiveAoe(0f, 1f, aoeVList[i]));
        StartCoroutine(WaitActiveAoe(0.5f, 1f, aoeVList[i-1]));
        StartCoroutine(WaitActiveAoe(0.5f, 1f, aoeVList[i+1]));
        StartCoroutine(WaitActiveAoe(1f, 1f, aoeVList[i - 2]));
        StartCoroutine(WaitActiveAoe(1f, 1f, aoeVList[i + 2]));
        StartCoroutine(WaitActiveAoe(1.5f, 1f, aoeVList[i - 3]));
        StartCoroutine(WaitActiveAoe(1.5f, 1f, aoeVList[i + 3]));

        //Aoe H
        StartCoroutine(WaitActiveAoe(2f, 1f, aoeHList[i]));
        StartCoroutine(WaitActiveAoe(2.5f, 1f, aoeHList[i-1]));
        StartCoroutine(WaitActiveAoe(2.5f, 1f, aoeHList[i+1]));
        StartCoroutine(WaitActiveAoe(3f, 1f, aoeHList[i - 2]));
        StartCoroutine(WaitActiveAoe(3f, 1f, aoeHList[i + 2]));
        StartCoroutine(WaitActiveAoe(3.5f, 1f, aoeHList[i - 3]));
        StartCoroutine(WaitActiveAoe(3.5f, 1f, aoeHList[i + 3]));

        //Aoe Circle
        StartCoroutine(WaitActiveAoe(4.5f, 2f, aoeCircleList[i-2]));
        StartCoroutine(WaitActiveAoe(4.5f, 2f, aoeCircleList[i - 4]));
        StartCoroutine(WaitActiveAoe(4.5f, 2f, aoeCircleList[i + 2]));
        StartCoroutine(WaitActiveAoe(4.5f, 2f, aoeCircleList[i + 4]));
        StartCoroutine(WaitActiveAoe(5.5f, 2f, aoeCircleList[i - 3]));
        StartCoroutine(WaitActiveAoe(5.5f, 2f, aoeCircleList[i - 1]));
        StartCoroutine(WaitActiveAoe(5.5f, 2f, aoeCircleList[i + 3]));
        StartCoroutine(WaitActiveAoe(5.5f, 2f, aoeCircleList[i + 1]));
        StartCoroutine(WaitActiveAoe(6.5f, 2f, aoeCircleList[i]));
    }

    IEnumerator WaitActiveAoe(float timeAoeAppear, float timeBoxCollideEnable, GameObject aoe)
    {
        yield return new WaitForSeconds(timeAoeAppear);  //Moment ou l'aoe apparait sur l'écran
        aoe.SetActive(true);

        yield return new WaitForSeconds(timeBoxCollideEnable); //Moment ou l'aoe tue le joueur 
        if (aoe.name.Contains("Circle")){aoe.GetComponent<CircleCollider2D>().enabled = true;}
        else                            {aoe.GetComponent<BoxCollider2D>().enabled    = true;}

        yield return new WaitForSeconds(1f); //Détruit l'aoe 1 seconde apres le kill
        aoe.SetActive(false);
    }

    int getTheNumber(string name)
    {
        int i;
        string lettre = null;
        if (name.Contains("Circle"))
        {
            lettre = name.Substring(9);
        }
        else
        {
            lettre = name.Substring(4);
        }

        switch (lettre)
        {
            case "1":
                i = 1;
                break;
            case "2":
                i = 2;
                break;
            case "3":
                i = 3;
                break;
            case "4":
                i = 4;
                break;
            case "5":
                i = 5;
                break;
            case "6":
                i = 6;
                break;
            case "7":
                i = 7;
                break;
            case "8":
                i = 8;
                break;
            case "9":
                i = 9;
                break;
            case "10":
                i = 10;
                break;
            default:
                i = 0;
                break;
        }

        return i;
    }
}
