using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follower : MonoBehaviour {

    public GameObject goToFollow;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = goToFollow.transform.position;
	}
}
