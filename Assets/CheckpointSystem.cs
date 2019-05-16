using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    //public static int checkpointIndex;
    public static Vector3 checkpointPosition;

    private bool wentThrough;

    public Vector3 intialPos;

    public static bool hasBeenAwaked;

    private void Awake()
    {
        if(hasBeenAwaked == false)
        {
            checkpointPosition = intialPos;
            hasBeenAwaked = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && wentThrough == false)
        {
                checkpointPosition = this.transform.position;
                wentThrough = true;         
        }
    }

    public static void RespawnCheckpoint(Transform playerTransform)
    {
        playerTransform.position = checkpointPosition;
    }
}
