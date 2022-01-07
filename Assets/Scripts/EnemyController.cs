using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;
    //public Rigidbody theRB;

    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime;
    private float chaseCounter;



    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position; // store initial position of enemy
    }

    // Update is called once per frame
    void Update()
    {
        // enemy will look around on Y axis for player, but will look up or down. Ensures enemy does not float up into air if player jumps
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!chasing)
        {
            // is enemy position less than diatance to chase? If so, chase!
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;
            }

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                Debug.Log("chaseCounter: " + chaseCounter);

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                    Debug.Log("Sending Enemy back to ttartPoint");
                }
            }
        }
        else
        {
            //transform.LookAt(targetPoint); //look at the transform position of the player controller instance
            //theRB.velocity = transform.forward * moveSpeed; //move the enemy forward by the moveSpeed

            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = targetPoint; // enemy keeps moving if outside distance to stop range
            }
            else
            {
                agent.destination = transform.position; // if within the distance to chase area, enemy stops moving
            }



            // stop chasing player if outside range of distance to lose
            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;
                chaseCounter = keepChasingTime;
            }

        }
    }
}
