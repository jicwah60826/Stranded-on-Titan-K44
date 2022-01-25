using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool chasing;
    public float distanceToChase = 10f;
    public float distanceToLose = 15f;
    public float distanceToStop;

    private Vector3 targetPoint;
    private Vector3 startPoint;

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
    public float fieldOfView; // angle amount from the player that enemy is allowed to fire within
    private float shotWaitCounter; //countdown timer for the waitBetweenShots
    private float shootTimeCounter; // countdown timer for the timeToShoot

    public Animator anim;

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
        // enemy will look around on Y axis for player, but will not look up or down. Ensures enemy does not float up into air if player jumps
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;


        if (!chasing)
        {
            // is enemy position less than distance to chase? If so, chase!
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;
                anim.SetBool("isMoving", true);

                fireCount = waitTimeToFire;
                shootTimeCounter = timeToShoot; // initialize the shootTimeCounter
                shotWaitCounter = waitBetweenShots; // initialize the shotWaitCounter
            }

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                Debug.Log("chaseCounter: " + chaseCounter);

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                    Debug.Log("Sending Enemy back to startPoint");
                }
            }

            // how much distance does enemy still have to player
            if (agent.remainingDistance < .25f)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
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
                agent.destination = transform.position; // if OUTSIDE the distance to chase area, enemy stops moving
            }

            // stop chasing player if outside range of distance to lose
            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;
                chaseCounter = keepChasingTime;
            }

            if (shotWaitCounter > 0)
            {
                // Player Moving again
                shotWaitCounter -= Time.deltaTime; //begin the shotWaitCounter
                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
                anim.SetBool("isMoving", true);
            }
            else
            {
                //Only allow the enemy to shoot if we are NOT paused OR the level is ending

                if (PlayerController.instance.gameObject.activeInHierarchy && !GameManager.instance.levelEnding)
                {

                    //shot wait counter has run out - enemy can shoot now
                    shootTimeCounter -= Time.deltaTime; // begin the shootTimeCounter

                    if (shootTimeCounter > 0)
                    {
                        // as long as the shoot time countdown clock has time on it, enemy can shoot
                        fireCount -= Time.deltaTime; // begin countdown timer between each shot

                        if (fireCount <= 0)
                        {
                            // when fireCount reaches 0, reset it back to fireRate
                            fireCount = fireRate;


                            // Enemy firepoint always rotates towards player. Ensure enemy firepoint is up towards player eyeline
                            firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.5f, 0f));

                            // get direct amount that player is from enemy
                            Vector3 targetDirection = PlayerController.instance.transform.position - transform.position;

                            // get the angle amount from our target direction to the player
                            float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

                            if (Mathf.Abs(angle) < fieldOfView) // Mathf.bs ensures we also feed in a positive value in the event the angle returned is a negative value
                            {
                                //Fire a bullet
                                Instantiate(bullet, firePoint.position, firePoint.rotation);
                                // Enemy firing animation

                                anim.SetTrigger("fireShot");
                                anim.SetBool("isMoving", false);
                            }
                        }
                        // enemy stops while shooting
                        agent.destination = transform.position; // freeze enemy in place while enemy is shooting
                    }

                    else
                    {
                        //shoot time counter has run out - enemy has run out of shooting time
                        // reset the shotWaitCounter so that enemy has to wait between shots again
                        shotWaitCounter = waitBetweenShots;
                    }

                }
                else
                {
                    // Player dead / no longer active: make enemies stop chasing and go back to spawn point
                    agent.destination = transform.position;
                }
            }
        }
    }
}
