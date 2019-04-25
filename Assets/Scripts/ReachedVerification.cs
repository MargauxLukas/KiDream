using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedVerification : MonoBehaviour
{
    public GPSAbility myGPS;


    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && myGPS.waypoints[myGPS.i].position == this.transform.position)
        {

            myGPS.i = myGPS.i + 1;

        }
    }






}
