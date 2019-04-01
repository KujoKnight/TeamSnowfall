using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{

    public Transform[] waypoints;
    public int destination;
    public NavMeshAgent agent;
    public GameObject player;

    private bool playerDetected = false;
    //private bool playerHit = false;

    // Use this for initialization
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        SetDestination();

    }

    // Update is called once per frame
    void Update()
    {

        if (!playerDetected)
        {
            // Checks if the npc has a path and calls the SetDestination function
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                SetDestination();
            }
        }
        else
        {
            //Sets the npc destination to the player and increases its speed
            agent.destination = player.transform.position;
            SetSpeed();
        }

    }

    private void SetDestination()
    {
        //Sets the destination of the enemy to waypoints
        agent.destination = waypoints[destination].position;

        destination = (destination + 1) % waypoints.Length;
    }

    //Sets the speed of the npc
    private void SetSpeed()
    {
        if (playerDetected)
        {
            agent.speed = 7;
        }
    }

    private void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            Debug.Log("Player Detected");
        }
    }
}