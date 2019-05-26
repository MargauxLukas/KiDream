using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromKeyboard : MonoBehaviour
{
    public GameObject player;

    public List<Transform> spawns = new List<Transform>();

	// Start
	void Start ()
    {
		
	}
	
	// Update
	void Update ()
    {
        SpawningPlayer();
	}

    public void SpawningPlayer()
    {
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            player.transform.position = spawns[0].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            player.transform.position = spawns[1].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            player.transform.position = spawns[2].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            player.transform.position = spawns[3].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            player.transform.position = spawns[4].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            player.transform.position = spawns[5].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            player.transform.position = spawns[6].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            player.transform.position = spawns[7].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            player.transform.position = spawns[8].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            player.transform.position = spawns[9].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            player.transform.position = spawns[10].transform.position;
        }
    }
}
