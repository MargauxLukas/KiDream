using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPositionPlayer : MonoBehaviour
{
    public GameObject player;
	// Use this for initialization
	void Start ()
    {
        player.transform.position = new Vector3(0f, 0f, 0f);
	}
}
