using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BAITAbility : MonoBehaviour
{

    public Transform myPlayer;
    public AIDestinationSetter myTarget;
    public GameObject myParticles;
    public CharacterController characterController;

    // Booléens Bait
    public float baitCooldownTime;
    public bool baitIsLaunched = false;
    private bool followingPlayer = true;

    // Start
    void Start ()
    {

    }
	
	// Update
	void Update ()
    {

        if(followingPlayer == true)
        {
            this.gameObject.transform.position = myPlayer.transform.position;
        }
        else
        {
            return;
        }

        if (Input.GetKeyDown("joystick 1 button 1") && characterController.reve == false && baitIsLaunched == false)
        {
            LaunchBait();
        }
    }



    public void LaunchBait()
    {
        followingPlayer = false;
        myParticles.SetActive(true);
        StartCoroutine(CoolDownBait());
        StartCoroutine(TimeBeforeDestroy());
    }


    IEnumerator CoolDownBait()
    {
        baitIsLaunched = true;
        yield return new WaitForSeconds(baitCooldownTime);
        baitIsLaunched = false;
    }

    IEnumerator TimeBeforeDestroy()
    {
        yield return new WaitForSeconds(3);
        myParticles.SetActive(false);
        followingPlayer = true;
        myTarget.target = null;

    }

}
