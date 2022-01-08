using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime;
    private float chaseCounter;

    public GameObject bullet; // bullet object to fire
    public Transform firePoint; // location to fire bullets from

    public float fireRate; // delay between each bullet
    public float waitTimeToFire; // amount of time to wait until firing on player once the chase begins
    private float fireCount; // countdown timer between each shot

    public float waitBetweenShots; // time to wait between each shot
    public float timeToShoot; // length of time that enemy continues shooting
    private float shotWaitCounter; //countdown timer for the waitBetweenShots
    private float shootTimeCounter; // countdown timer for the timeToShoot




    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position; // store initial position of enemy
        shootTimeCounter = timeToShoot; // initialize the shootTimeCounter
        shotWaitCounter = waitBetweenShots; // initialize the shotWaitCounter
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

                fireCount = waitTimeToFire;
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
            // Begin chasing the player

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

            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime; //begin the shotWaitCounter
                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
            }
            else
            {
                //shot wait counter has run out - enemy can shoot now
                shootTimeCounter -= Time.deltaTime; // begin the shootTimeCounter

                if (shootTimeCounter > 0)
                {
                    // as long as the shoot time countdown clock has time on it, enemy can
                    fireCount -= Time.deltaTime; // begin countdown timer between each shot

                    if (fireCount <= 0)
                    {
                        // when fireCount reaches 0, reset it back to fireRate
                        fireCount = fireRate;
                        //Fire a bullet
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                    }
                }

                else
                {
                    //shoot time counter has run out - enemy has run out of shooting time
                    // reset the shotWaitCounter so that enemy has to wait between shots again
                    shotWaitCounter = waitBetweenShots;
                }
            }
        }
    }
}
