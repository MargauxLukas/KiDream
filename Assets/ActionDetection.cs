using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDetection : MonoBehaviour {

    public CharacterController myPlayer;
    public IndicatorList indicatorList;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "UIDetectionTag")
        {
            indicatorList = collision.GetComponentInChildren<IndicatorList>();
            ReactionToWave rtw = collision.GetComponentInParent<ReactionToWave>();

            if (myPlayer.reve == true)
            {
                foreach(GameObject go in indicatorList.iconList)
                {
                    if(go.name.Contains("Corrupted") == true)
                    {
                        go.SetActive(false);
                    }
                }

                switch (rtw.canBePushed)
                {
                    case true:
                        indicatorList.iconList[0].SetActive(true);
                        break;

                    case false:
                        indicatorList.iconList[0].SetActive(false);
                        break;
                }

                switch (rtw.canBePulled)
                {
                    case true:
                        indicatorList.iconList[1].SetActive(true);
                        break;

                    case false:
                        indicatorList.iconList[1].SetActive(false);
                        break;
                }

                switch (rtw.canBeActivated)
                {
                    case true:
                        indicatorList.iconList[2].SetActive(true);
                        break;

                    case false:
                        indicatorList.iconList[2].SetActive(false);
                        break;
                }
            }
            else
            {
                foreach (GameObject go in indicatorList.iconList)
                {
                    if (go.name.Contains("Corrupted") == false)
                    {
                        go.SetActive(false);
                    }
                }

                switch (rtw.canBePushCorrupted)
                {
                    case true:
                        indicatorList.iconList[3].SetActive(true);
                        break;

                    case false:
                        indicatorList.iconList[3].SetActive(false);
                        break;
                }

                switch (rtw.canBePullCorrupted)
                {
                    case true:
                        indicatorList.iconList[4].SetActive(true);
                        break;

                    case false:
                        indicatorList.iconList[4].SetActive(false);
                        break;
                }

                switch (rtw.canBeActivateCorrupted)
                {
                    case true:
                        indicatorList.iconList[5].SetActive(true);
                        break;

                    case false:
                        indicatorList.iconList[5].SetActive(false);
                        break;
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        indicatorList = collision.GetComponentInChildren<IndicatorList>();

        if(collision.name.Contains("ParticleCollisionLayer"))
        {
            foreach (GameObject go in indicatorList.iconList)
            {
                go.SetActive(false);
            }
        }
    }

}
