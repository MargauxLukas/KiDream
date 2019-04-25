using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_ParticleWaypoints : MonoBehaviour
{

    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 2f;

    [SerializeField]
    int waypointIndex = 0;

    

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            InvokeRepeating("GPS", 0.01f, 0.01f);
        }

    }


    private void GPS()
    {

        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);


        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
            CancelInvoke();
            return;
        }

        else if(waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }

    }

    private void MyRaycast()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("Walls"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("Walls"));
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("Walls"));
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("Walls"));
    }



}
