using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GPSAbility : MonoBehaviour
{

    public GameObject myParticles;
    public Transform myPlayer;
    public CharacterController characterController;

    public int i = 0;
    public float gpsCooldownTime;

    private AIDestinationSetter targetManager;

    // Booléens GPS
    public bool isLaunching = false;

    public List<Transform> waypoints = new List<Transform>();


	// Start
	void Start ()
    {
        targetManager = this.GetComponent<AIDestinationSetter>();
	}
	
	// Update
	void Update ()
    {

        if (isLaunching == false)
        {
            this.gameObject.transform.position = myPlayer.transform.position;
        }
        else
        {
            return;
        }

        LaunchGPS();

    }


    public void LaunchGPS()
    {
        if (Input.GetKeyDown("joystick 1 button 1") && characterController.reve == true && isLaunching == false)
        {
            isLaunching = true;
            myParticles.SetActive(true);
            targetManager.target = waypoints[i];
            StartCoroutine(CoolDownGPS());
            StartCoroutine(TimeBeforeDestroy());
        }
    }

    IEnumerator CoolDownGPS()
    {
        yield return new WaitForSeconds(gpsCooldownTime);
        isLaunching = false;
    }


    IEnumerator TimeBeforeDestroy()
    {
        yield return new WaitForSeconds(3);
        targetManager.target = null;
        myParticles.SetActive(false);
    }




}
